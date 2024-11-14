using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PrivateClinic.Model;
using PrivateClinic.View.MessageBox;
using PrivateClinic.View.QuanLiKhoThuoc;
using PrivateClinic.ViewModel.OtherViewModels;

namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    public class ThemThuocMoiViewModel: BaseViewModel
    {

        private ObservableCollection<DVT> _dvt;

        public ObservableCollection<DVT> dvt
        {
            get => _dvt;
            set { _dvt = value; OnPropertyChanged(nameof(dvt)); }

        }
        private ObservableCollection<CACHDUNG> _cd;

        public ObservableCollection<CACHDUNG> cd
        {
            get => _cd;
            set { _cd = value; OnPropertyChanged(nameof(cd)); }
        }
        private ObservableCollection<LOAITHUOC> _lt;

        public ObservableCollection<LOAITHUOC> lt
        {
            get => _lt;
            set { _lt = value; OnPropertyChanged(nameof(lt)); }

        }
        private ObservableCollection<DVT> _donvitinh;

        public ObservableCollection<DVT> donvitinh
        {
            get => _donvitinh;
            set { _donvitinh = value; OnPropertyChanged(nameof(donvitinh)); }

        }
        private ObservableCollection<CACHDUNG> _cachdung;

        public ObservableCollection<CACHDUNG> cachdung
        {
            get => _cachdung;
            set { _cachdung = value; OnPropertyChanged(nameof(cachdung)); }

        }
        private ObservableCollection<LOAITHUOC> _loaithuoc;

        public ObservableCollection<LOAITHUOC> loaithuoc
        {
            get => _loaithuoc;
            set { _loaithuoc = value; OnPropertyChanged(nameof(loaithuoc)); }

        }
        private List<string> _TenDVTs;
        public List<string> TenDVTs
        {
            get { return _TenDVTs; }
            set
            {
                _TenDVTs = value;
                OnPropertyChanged(nameof(TenDVTs));
            }
        }
        private List<string> _CachDung;
        public List<string> CachDung
        {
            get { return _CachDung; }
            set
            {
                _CachDung = value;
                OnPropertyChanged(nameof(CachDung));
            }
        }
        private List<string> _LoaiThuoc;
        public List<string> LoaiThuoc
        {
            get { return _LoaiThuoc; }
            set
            {
                _LoaiThuoc = value;
                OnPropertyChanged(nameof(LoaiThuoc));
            }
        }
        private ObservableCollection<THUOC> _thuoc;

        public ObservableCollection<THUOC> Thuoc
        {
            get => _thuoc;
            set { _thuoc = value; OnPropertyChanged(nameof(Thuoc)); }

        }
        private ObservableCollection<ThuocDTO> _thuocdto;

        public ObservableCollection<ThuocDTO> Thuocdto
        {
            get => _thuocdto;
            set { _thuocdto = value; OnPropertyChanged(nameof(Thuocdto)); }

        }
        private ObservableCollection<CT_PNT> _ctphieunhap;

        public ObservableCollection<CT_PNT> ctphieunhap
        {
            get => _ctphieunhap;
            set { _ctphieunhap = value; OnPropertyChanged(nameof(ctphieunhap)); }

        }
        private ObservableCollection<THAMSO> _thamso;

        public ObservableCollection<THAMSO> thamso
        {
            get => _thamso;
            set { _thamso = value; OnPropertyChanged(nameof(thamso)); }

        }
        private ObservableCollection<PHIEUNHAPTHUOC> _phieunhap;

        public ObservableCollection<PHIEUNHAPTHUOC> phieunhap
        {
            get => _phieunhap;
            set { _phieunhap = value; OnPropertyChanged(nameof(phieunhap)); }
        }

        public ICommand LoadCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ThemThuocMoiViewModel()
        {
            LoadCommand = new RelayCommand<ThemThuocMoiView>((p) => true, (p) => _LoadCommand(p));
            SaveCommand = new RelayCommand<ThemThuocMoiView>((p) => true, (p) => _SaveCommand(p));

        }
        private void _LoadCommand(ThemThuocMoiView p)
        {
            dvt = new ObservableCollection<DVT>(DataProvider.Ins.DB.DVTs);
            TenDVTs = dvt.Select(x => x.TenDVT).ToList();
            cd = new ObservableCollection<CACHDUNG>(DataProvider.Ins.DB.CACHDUNGs);
            CachDung = cd.Select(x => x.TenCachDung).ToList();
            lt = new ObservableCollection<LOAITHUOC>(DataProvider.Ins.DB.LOAITHUOCs);
            LoaiThuoc = lt.Select(x=>x.TenLoaiThuoc).ToList();


        }
        void _SaveCommand(ThemThuocMoiView paramater)
        {
            if (string.IsNullOrEmpty(paramater.SL.Text) || string.IsNullOrEmpty(paramater.DonGiaNhap.Text) || paramater.TenDVTcbx.SelectedItem == null || paramater.CachDungcbx.SelectedItem == null || string.IsNullOrEmpty(paramater.TenThuoc.Text) || paramater.CachDungcbx.SelectedItem == null)
            {
                OkMessageBox ok = new OkMessageBox("Thông báo", "Chưa đủ thông tin");
                ok.ShowDialog();
            }
            else if (!int.TryParse(paramater.SL.Text, out int slValue) || slValue <= 0)
            {
                OkMessageBox ok = new OkMessageBox("Thông báo", "Số lượng không hợp lệ");
                ok.ShowDialog();
            }
            else if (!double.TryParse(paramater.DonGiaNhap.Text, out double dgValue)|| dgValue <0)
            {
                OkMessageBox ok = new OkMessageBox("Thông báo", "Đơn giá nhập không hợp lệ");
                ok.ShowDialog();
            }
            else
            {
                Thuoc = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOCs);
                bool thuocDaTonTai = false;
                foreach (var t in Thuoc)
                {
                    if (t.TenThuoc.Equals(paramater.TenThuoc.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        OkMessageBox ok = new OkMessageBox("Thông báo", "Thuốc này đã có trong kho");
                        ok.ShowDialog(); 
                        thuocDaTonTai = true;
                        break;
                    }
                }

                if (!thuocDaTonTai)
                {
                    YesNoMessageBox h = new YesNoMessageBox("THÔNG BÁO", "Bạn muốn thêm thuốc mới vào kho ?");
                    h.ShowDialog();
                    if (h.DialogResult == true)
                    {
                        donvitinh = new ObservableCollection<DVT>(DataProvider.Ins.DB.DVTs);
                        cachdung = new ObservableCollection<CACHDUNG>(DataProvider.Ins.DB.CACHDUNGs);
                        Thuoc = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOCs);
                        phieunhap = new ObservableCollection<PHIEUNHAPTHUOC>(DataProvider.Ins.DB.PHIEUNHAPTHUOCs);
                        ctphieunhap = new ObservableCollection<CT_PNT>(DataProvider.Ins.DB.CT_PNT);
                        loaithuoc = new ObservableCollection<LOAITHUOC>(DataProvider.Ins.DB.LOAITHUOCs);
                        thamso = new ObservableCollection<THAMSO>(DataProvider.Ins.DB.THAMSOes);

                        THUOC newthuoc = new THUOC();
                        {
                            newthuoc.MaThuoc = int.Parse(paramater.MaThuoc.Text.Substring(3));
                            newthuoc.TenThuoc = paramater.TenThuoc.Text;
                            newthuoc.DonGiaNhap = dgValue;
                            newthuoc.SoLuong = slValue;
                            foreach (var ts in thamso)
                            {
                                if (ts.MaThamSo == 3)
                                {
                                    newthuoc.DonGiaBan = newthuoc.DonGiaNhap * ts.GiaTri;
                                }
                            }

                            foreach (var dvt in donvitinh)
                            {
                                if (dvt.TenDVT == paramater.TenDVTcbx.SelectedItem.ToString())
                                {
                                    newthuoc.MaDVT = dvt.MaDVT;
                                }
                            }
                            foreach (var lt in loaithuoc)
                            {
                                if (lt.TenLoaiThuoc == paramater.LoaiThuoccbx.SelectedItem.ToString())
                                {
                                    newthuoc.MaLoaiThuoc = lt.MaLoaiThuoc;
                                }
                            }
                            foreach (var cd in cachdung)
                            {
                                if (cd.TenCachDung == paramater.CachDungcbx.SelectedItem.ToString())
                                {
                                    newthuoc.MaCachDung = cd.MaCachDung;
                                }
                            }
                            int nextPN;
                            nextPN = DataProvider.Ins.DB.PHIEUNHAPTHUOCs.Max(t => t.SoPhieuNhap) + 1;
                            PHIEUNHAPTHUOC pnt = new PHIEUNHAPTHUOC();

                            pnt.SoPhieuNhap = nextPN;
                            pnt.NgayNhap = paramater.NgayNhap.SelectedDate;
                            DataProvider.Ins.DB.PHIEUNHAPTHUOCs.Add(pnt);
                            DataProvider.Ins.DB.THUOCs.Add(newthuoc);
                            DataProvider.Ins.DB.SaveChanges();
                            OkMessageBox ok = new OkMessageBox("Thông báo", "Thêm thành công");
                            ok.ShowDialog();
                        }
                    }
                }


            }

        }
    }
}
