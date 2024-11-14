using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateClinic.Model;
using System.Windows.Input;
using PrivateClinic.ViewModel.OtherViewModels;
using PrivateClinic.View.QuanLiKhoThuoc;
using System.Windows;
using static System.Net.WebRequestMethods;
using System.Globalization;
using PrivateClinic.View.QuanLiTiepDon;

namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    public class KhoThuocViewModel : BaseViewModel
    {
        private ObservableCollection<THUOC> _thuoc;
        public ObservableCollection<THUOC> Thuoc
        {
            get => _thuoc;
            set { _thuoc = value; OnPropertyChanged(nameof(Thuoc)); }

        }

        private ObservableCollection<ThuocDTO> _listMed;
        public ObservableCollection<ThuocDTO> listMed
        {
            get => _listMed;
            set
            {
                _listMed = value; OnPropertyChanged(nameof(listMed));
            }

        }

        private ObservableCollection<CT_PNT> _ctphieunhap;
        public ObservableCollection<CT_PNT> ctphieunhap
        {
            get => _ctphieunhap;
            set { _ctphieunhap = value; OnPropertyChanged(nameof(ctphieunhap)); }

        }

        private ObservableCollection<PHIEUNHAPTHUOC> _phieunhap;
        public ObservableCollection<PHIEUNHAPTHUOC> phieunhap
        {
            get => _phieunhap;
            set { _phieunhap = value; OnPropertyChanged(nameof(phieunhap)); }

        }

        private ObservableCollection<DVT> _donvitinh;
        public ObservableCollection<DVT> donvitinh
        {
            get => _donvitinh;
            set { _donvitinh = value; OnPropertyChanged(nameof(donvitinh)); }

        }

        private int soLuongThuoc;
        public int SoLuongThuoc
        {
            get => soLuongThuoc;
            set
            {
                if (soLuongThuoc != value)
                {
                    soLuongThuoc = value;
                    OnPropertyChanged(nameof(SoLuongThuoc));
                }
            }
        }


        public ICommand AddMedicineCommand { get; set; }
        public ICommand EditMedicineCommand { get; set; }
        public ICommand DetailMedicineCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public KhoThuocViewModel()
        {
            LoadData();
            AddMedicineCommand = new RelayCommand<KhoThuocView>((p) => { return p == null ? false : true; }, (p) => _AddMedicineCommand(p));
            EditMedicineCommand = new RelayCommand<ThuocDTO>((p) => { return p == null ? false : true; }, (p) => _EditMedicineCommand(p));
            DetailMedicineCommand = new RelayCommand<KhoThuocView>((p) => { return p == null ? false : true; }, (p) => _DetailMedicineCommand(p));
            SearchCommand = new RelayCommand<KhoThuocView>((p) => { return p == null ? false : true; }, (p) => _SearchCommand(p));
        }
        void LoadData()
        {
            Thuoc = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOCs);
            donvitinh = new ObservableCollection<DVT>(DataProvider.Ins.DB.DVTs);
            phieunhap = new ObservableCollection<PHIEUNHAPTHUOC>(DataProvider.Ins.DB.PHIEUNHAPTHUOCs);
            ctphieunhap = new ObservableCollection<CT_PNT>(DataProvider.Ins.DB.CT_PNT);
            listMed = new ObservableCollection<ThuocDTO>();
            int stt = 1;
            foreach (var thuoc in Thuoc)
            {
                foreach (var dvt in donvitinh)
                {
                    if (thuoc.MaDVT == dvt.MaDVT)
                    {
                  
                        string maThuoc = "MED" + thuoc.MaThuoc.ToString();
                       
                        ThuocDTO thuocDTO = new ThuocDTO()
                        {
                            STT = stt,
                            DVT = dvt.TenDVT,
                            MaThuoc = maThuoc,
                            TenThuoc = thuoc.TenThuoc,
                            Gia = thuoc.DonGiaNhap,
                            SL = thuoc.SoLuong
                        };
                        foreach (var ct in ctphieunhap)
                        {
                            if (thuoc.MaThuoc == ct.MaThuoc)
                            {
                                foreach (var ngaynhap in phieunhap)
                                {
                                    if (ct.SoPhieuNhap == ngaynhap.SoPhieuNhap)
                                    {
                                        thuocDTO.NgayNhap = ngaynhap.NgayNhap;
                                        break;
                                    }
                                }
                                break;
                            }
                            
                        }
                        listMed.Add(thuocDTO);
                        stt++;
                        break;
                    }
                }
            }
            SoLuongThuoc = listMed.Count();
        }

        void _SearchCommand(KhoThuocView parameter)
        {
            ObservableCollection<ThuocDTO> temp = new ObservableCollection<ThuocDTO>();
            if (!string.IsNullOrEmpty(parameter.txbSearch.Text))
            {
                string searchKeyword = parameter.txbSearch.Text.ToLower();
                temp = new ObservableCollection<ThuocDTO>(listMed.Where(s => s.TenThuoc.ToLower().Contains(searchKeyword)));
            }
            else
            {
                temp = new ObservableCollection<ThuocDTO>(listMed);
            }
            parameter.MedicineListView.ItemsSource = temp;
        }

        void _EditMedicineCommand(ThuocDTO selectedItem)
        {
            if (selectedItem != null)
            {
                SuaThongTinThuocViewModel.Instance.EditThuocView = new ThayDoiThongTinThuocView();
                SuaThongTinThuocViewModel.Instance.EditThuocView.TenThuoc.Text = selectedItem.TenThuoc.ToString();
                SuaThongTinThuocViewModel.Instance.EditThuocView.MaThuoc.Text = selectedItem.MaThuoc.ToString();
                SuaThongTinThuocViewModel.Instance.EditThuocView.DonGiaNhap.Text = selectedItem.Gia.ToString();
                SuaThongTinThuocViewModel.Instance.EditThuocView.SoLuong.Text = selectedItem.SL.ToString();
                //SuaThongTinThuocViewModel.Instance.EditThuocView.NgayNhap.SelectedDate = selectedItem.NgayNhap;
                SuaThongTinThuocViewModel.Instance.EditThuocView.NgayNhap.SelectedDate = DateTime.Now;
                SuaThongTinThuocViewModel.Instance.EditThuocView.TenDVTcbx.SelectedItem =
                SuaThongTinThuocViewModel.Instance.EditThuocView.TenDVTcbx.Items.Cast<string>().FirstOrDefault(item => item == selectedItem.DVT.ToString());
                double mainWindowRightEdge = Application.Current.MainWindow.Left + Application.Current.MainWindow.Width;
                SuaThongTinThuocViewModel.Instance.EditThuocView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                SuaThongTinThuocViewModel.Instance.EditThuocView.ShowDialog();
                LoadData();
            }
        }

       
        public void _AddMedicineCommand(KhoThuocView parameter)
        {
            NhapThuocView addMedicineView = new NhapThuocView();
            double mainWindowRightEdge = Application.Current.MainWindow.Left + Application.Current.MainWindow.Width;
            addMedicineView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addMedicineView.ShowDialog();
            LoadData() ;
        }
        public void _DetailMedicineCommand(KhoThuocView parameter)
        {
            //ctphieunhap = new ObservableCollection<CT_PNT>(DataProvider.Ins.DB.CT_PNT);
            //phieunhap = new ObservableCollection<PHIEUNHAPTHUOC>(DataProvider.Ins.DB.PHIEUNHAPTHUOCs);
            ThongTinThuocView detail = new ThongTinThuocView();
            ThuocDTO selectedThuocDTO = (ThuocDTO)parameter.MedicineListView.SelectedItem;
            THUOC temp = Thuoc.FirstOrDefault(t => t.TenThuoc == selectedThuocDTO.TenThuoc);
            detail.DonGia.Text = String.Format("{0:0,0}", temp.DonGiaNhap);
            detail.DonGiaBan.Text = String.Format("{0:0,0}", temp.DonGiaBan);
            detail.TenThuoc.Text = temp.TenThuoc;
            detail.MaThuoc.Text = "MED" + temp.MaThuoc.ToString();
            detail.DVT.Text = temp.DVT.TenDVT.ToString();
            detail.SL.Text = temp.SoLuong.ToString();
            detail.NgNhap.Text = selectedThuocDTO.NgayNhap.Value.ToString("dd/MM/yyyy");
            //if (ctphieunhap != null)
            //{
            //    CT_PNT ct = ctphieunhap.FirstOrDefault(t => t.MaThuoc == temp.MaThuoc);
            //    if (ct != null)
            //    {
            //        PHIEUNHAPTHUOC pnt = phieunhap.FirstOrDefault(a => a.SoPhieuNhap == ct.SoPhieuNhap);
            //        if (pnt != null)
            //        {
            //            if (pnt.NgayNhap.HasValue)
            //            {
            //                detail.NgNhap.Text = pnt.NgayNhap.Value.ToString("dd/MM/yyyy");
            //            }
            //            else
            //            {
            //                detail.NgNhap.Text = string.Empty; // Hoặc hiển thị một giá trị mặc định nếu cần
            //            }
            //        }
            //        else
            //        {
            //            // Xử lý trường hợp pnt là null
            //            detail.NgNhap.Text = "Chưa có ngày nhập";
            //        }
            //    }
            //    else
            //    {
            //        // Xử lý trường hợp ct là null
            //        detail.NgNhap.Text = "Chưa có ngày nhập";
            //    }
            //}
            //else
            //{
            //    // Xử lý trường hợp ctphieunhap là null
            //    detail.NgNhap.Text = "Chưa có ngày nhập";
            //}
            detail.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            detail.ShowDialog();
        }
    }
}
