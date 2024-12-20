using PrivateClinic.Model;
using PrivateClinic.View.MessageBox;
using PrivateClinic.View.QuanLiKhoThuoc;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    public class SuaThongTinThuocViewModel : BaseViewModel
    {
        public static SuaThongTinThuocViewModel Instance { get; set; } = new SuaThongTinThuocViewModel();

        public ThayDoiThongTinThuocView EditThuocView { get; set; }
        private ThayDoiThongTinThuocView EditThuocView2 { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand SetEditThuocView { get; set; }
        public ICommand LoadCommand { get; set; }
     
        private ObservableCollection<DVT> _dvt;

        public ObservableCollection<DVT> dvt
        {
            get => _dvt;
            set { _dvt = value; OnPropertyChanged(nameof(dvt)); }

        }
        private ObservableCollection<string> _tenDVTs;
        public ObservableCollection<string> tenDVTs
        {
            get { return _tenDVTs; }
            set
            {
                _tenDVTs = value;
                OnPropertyChanged(nameof(tenDVTs));
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

        public SuaThongTinThuocViewModel()
        {
            LoadCommand = new RelayCommand<ThayDoiThongTinThuocView>((p) => true, (p) => _LoadCommand(p));
            CancelCommand = new RelayCommand<ThayDoiThongTinThuocView>((p) => true, p => _CancelCommand(p));
            SaveCommand = new RelayCommand<ThayDoiThongTinThuocView>((p) => true, (p) => _SaveCommand(p));
            
            
            SetEditThuocView = new RelayCommand<ThayDoiThongTinThuocView>((p) => true, (p) => _SetEditThuocView(p));
        }

        public void _SetEditThuocView(ThayDoiThongTinThuocView view)
        {
            if (EditThuocView == null)
            {
                EditThuocView = view;
                EditThuocView2 = EditThuocView;
            }
        }
         private void _LoadCommand(ThayDoiThongTinThuocView p)       
        {
            dvt = new ObservableCollection<DVT>(DataProvider.Ins.DB.DVTs);
            TenDVTs = dvt.Select(x => x.TenDVT).ToList();


        }
        void _SaveCommand(ThayDoiThongTinThuocView paramater)
        {
            var p = new ThayDoiThongTinThuocView();
            p.TenThuoc.Text = SuaThongTinThuocViewModel.Instance.EditThuocView.TenThuoc.Text;
            p.NgayNhap.SelectedDate = SuaThongTinThuocViewModel.Instance.EditThuocView.NgayNhap.SelectedDate;
            p.DonGiaNhap.Text = SuaThongTinThuocViewModel.Instance.EditThuocView.DonGiaNhap.Text;
            p.TenDVTcbx.Text = SuaThongTinThuocViewModel.Instance.EditThuocView.TenDVTcbx.Text;
            p.SoLuong.Text = SuaThongTinThuocViewModel.Instance.EditThuocView.SoLuong.Text;
            p.MaThuoc.Text = SuaThongTinThuocViewModel.Instance.EditThuocView.MaThuoc.Text;

            if (string.IsNullOrEmpty(p.TenThuoc.Text) || p.NgayNhap.SelectedDate == null || string.IsNullOrEmpty(p.DonGiaNhap.Text) || string.IsNullOrEmpty(p.TenDVTcbx.SelectedItem.ToString()) || string.IsNullOrEmpty(p.SoLuong.Text))
            {
                OkMessageBox mb =new OkMessageBox("Thông báo", "Bạn chưa nhập đủ thông tin.");
                mb.ShowDialog();
            }
            else if (!int.TryParse(paramater.SoLuong.Text, out int slValue))
            {
                OkMessageBox mb = new OkMessageBox("Thông báo", "Số lượng phải là số!");
                mb.ShowDialog();
            }
            else if (!double.TryParse(paramater.DonGiaNhap.Text, out double dgValue))
            {
                OkMessageBox mb = new OkMessageBox("Thông báo", "Đơn giá nhập phải là số!");
                mb.ShowDialog();
            }
            else
            {
                YesNoMessageBox result = new YesNoMessageBox("Thông báo", "Bạn muốn lưu thông tin thuốc?");
                if (result.DialogResult == true)
                {
                    int maThuoc = int.Parse(p.MaThuoc.Text.Substring(3));
                    THUOC thuoc = DataProvider.Ins.DB.THUOCs.FirstOrDefault(t => t.MaThuoc == maThuoc);

                    if (thuoc != null)
                    {
                        thuoc.TenThuoc = p.TenThuoc.Text;
                        //thuoc.NgayNhap = (DateTime)p.NgayNhap.SelectedDate;
                        thuoc.DonGiaNhap = double.Parse(p.DonGiaNhap.Text);
                        //thuoc.TenDVT = p.TenDVT.Text;
                        thuoc.SoLuong = int.Parse(p.SoLuong.Text);

                        // Tìm hoặc thêm mới đơn vị tính (DVT)
                        //var dvt = DataProvider.Ins.DB.DVTs.FirstOrDefault(d => d.TenDVT == p.TenDVT.Text);
                      
                        var ctpnt = DataProvider.Ins.DB.CT_PNT.FirstOrDefault(ct => ct.MaThuoc == maThuoc);
                        var pnth = DataProvider.Ins.DB.PHIEUNHAPTHUOCs.FirstOrDefault(pn => pn.SoPhieuNhap == ctpnt.SoPhieuNhap);

                        var donvi = DataProvider.Ins.DB.DVTs.FirstOrDefault(dv => dv.MaDVT == thuoc.MaDVT);
                        //DVT dvt = dvtinh.FirstOrDefault(d => d.MaDVT == thuoc.MaDVT);
                        
                        var ma = DataProvider.Ins.DB.DVTs.FirstOrDefault(a => a.TenDVT==p.TenDVTcbx.Text);
                        thuoc.MaDVT = ma.MaDVT;

                        ctphieunhap = new ObservableCollection<CT_PNT>(DataProvider.Ins.DB.CT_PNT);
                        phieunhap = new ObservableCollection<PHIEUNHAPTHUOC>(DataProvider.Ins.DB.PHIEUNHAPTHUOCs);

                        //var ngaynhap = DataProvider.Ins.DB.PHIEUNHAPTHUOCs.FirstOrDefault(n => n.NgayNhap == p.NgayNhap.SelectedDate);

                        if (ctphieunhap != null)
                        {
                            CT_PNT ct = ctphieunhap.FirstOrDefault(t => t.MaThuoc == maThuoc);
                            if (ct != null)
                            {
                                PHIEUNHAPTHUOC pnt = phieunhap.FirstOrDefault(a => a.SoPhieuNhap == ct.SoPhieuNhap);
                                if (pnt != null)
                                {
                                    pnt.NgayNhap = (DateTime)p.NgayNhap.SelectedDate;
                                }
                                else
                                {
                                    pnt.NgayNhap = (DateTime)p.NgayNhap.SelectedDate;

                                }
                            }
                        }

                        //    DateTime ngayNhap = p.NgayNhap.SelectedDate.Value;
                        //var phieuNhap = DataProvider.Ins.DB.PHIEUNHAPTHUOCs.FirstOrDefault(pn => pn.NgayNhap == ngayNhap);

                        DataProvider.Ins.DB.SaveChanges();
                        OkMessageBox ok = new OkMessageBox("Thông báo", "Sửa thành công");
                        ok.ShowDialog();

                        KhoThuocView quanLiThuocView = new KhoThuocView();
                        quanLiThuocView.MedicineListView.ItemsSource = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOCs);
                        quanLiThuocView.MedicineListView.Items.Refresh();
                        paramater.Close();
                    }
                    else
                    {
                        OkMessageBox ok = new OkMessageBox("Thông báo", "Không tìm thấy thuốc để cập nhật");
                        ok.ShowDialog();
                    }
                }
            }
        }

        void _CancelCommand(ThayDoiThongTinThuocView paramater)
        {
            paramater.Close();
        }
    }
}
