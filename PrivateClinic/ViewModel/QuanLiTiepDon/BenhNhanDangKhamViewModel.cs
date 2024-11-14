using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateClinic.ViewModel.OtherViewModels;
using PrivateClinic.View.QuanLiTiepDon;
using System.Windows.Input;
using PrivateClinic.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Media3D;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Resources;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls;
using PrivateClinic.View.MessageBox;


namespace PrivateClinic.ViewModel.QuanLiTiepDon
{
    public class BenhNhanDangKhamViewModel : BaseViewModel
    {
        #region Các Command và Property
        private ObservableCollection<THUOC> _listMed;
        public ObservableCollection<THUOC> listMed { get => _listMed; set { _listMed = value; OnPropertyChanged(); } }

        //Lấy danh sách  thuốc DTO
        private ObservableCollection<ThuocDTO> listThuoc;
        public ObservableCollection<ThuocDTO> ListThuoc
        {
            get => listThuoc;
            set
            {
                listThuoc = value;
                OnPropertyChanged(nameof(ListThuoc));
            }
        }

        private ObservableCollection<ThuocDTO> listThuocView;
        public ObservableCollection<ThuocDTO> ListThuocView
        {
            get => listThuocView;
            set
            {
                listThuocView = value;
                OnPropertyChanged(nameof(ListThuocView));
                SoLuongThuocDangChon = ListThuocView.Count;
            }
        }
        //Lấy danh sách các loại bệnh
        private ObservableCollection<LOAIBENH> listloaibenh;
        public ObservableCollection<LOAIBENH> ListLoaiBenh
        {
            get => listloaibenh;
            set
            {
                listloaibenh = value;
                OnPropertyChanged(nameof(ListLoaiBenh));
            }
        }
        //Lấy đối tượng LoaiBenh
        private LOAIBENH selectedLoaiBenh;
        public LOAIBENH SelectedLoaiBenh
        {
            get => selectedLoaiBenh;
            set
            {
                selectedLoaiBenh = value;
                OnPropertyChanged(nameof(SelectedLoaiBenh));
            }
        }
        //Họ tên bác sĩ
        private string hoten;
        public string Hoten
        {
            get => hoten;
            set
            {
                hoten = value;
                OnPropertyChanged(nameof(Hoten));
            }
        }
        private int idbacsi;
        public int IDBacSi
        {
            get => idbacsi;
            set
            {
                idbacsi = value;
                OnPropertyChanged(nameof(IDBacSi));
            }
        }
        private NGUOIDUNG user;
        public NGUOIDUNG User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }


        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditThuocCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        private int soLuongThuocDangChon;
        public int SoLuongThuocDangChon
        {
            get => soLuongThuocDangChon;
            set
            {
                soLuongThuocDangChon = value;
                OnPropertyChanged(nameof(SoLuongThuocDangChon));
            }
        }
        private bool _isBtnDeleteVisible = false;
        public bool IsBtnDeleteVisible
        {
            get { return _isBtnDeleteVisible; }
            set
            {
                _isBtnDeleteVisible = value;
                OnPropertyChanged(nameof(IsBtnDeleteVisible));
            }
        }
        private bool _isBtnEditVisible = false;
        public bool isBtnEditVisible
        {
            get { return _isBtnEditVisible; }
            set
            {
                _isBtnEditVisible = value;
                OnPropertyChanged(nameof(isBtnEditVisible));
            }
        }
        #endregion
        public BenhNhanDangKhamViewModel()
        {
            ListLoaiBenh = new ObservableCollection<LOAIBENH>(DataProvider.Ins.DB.LOAIBENHs);
            ThongTinND();
            listMed = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOCs);
            AddThuoc();
            EditThuoc();
            ListThuoc = new ObservableCollection<ThuocDTO>();
            DeleteCommand = new RelayCommand<ThuocDTO>((p) => { return p == null ? false : true; }, (p) => _DeleteCommand(p));
            SaveCommand = new ViewModelCommand(Save);

        }

        #region Chức năng thêm thuốc
        void AddThuoc()
        {
            AddCommand = new ViewModelCommand(showAdd);
        }
        private void showAdd(object obj)
        {
            ThemThuocChoBenhNhanView view = new ThemThuocChoBenhNhanView();
            view.ShowDialog();
            LoadData();
        }
        #endregion

        #region Chức năng sửa thuốc
        void EditThuoc()
        {
            EditThuocCommand = new ViewModelCommand(ShowWDEditThuoc);
        }
        private void ShowWDEditThuoc(object obj)
        {
            ThuocDTO thuoc = obj as ThuocDTO;
            SuaThongTinDonThuocView view = new SuaThongTinDonThuocView((ThuocDTO)obj);
            view.ShowDialog();
            LoadData();
        }

        #endregion
        void _DeleteCommand(ThuocDTO selectedItem)
        {
            YesNoMessageBox h = new YesNoMessageBox("THÔNG BÁO", "Bạn muốn xóa thuốc này không ?");
            h.ShowDialog();
            if (h.DialogResult == true)
            {
                if (selectedItem != null)
                {
                    Const.ListThuocTemp.Remove(selectedItem);
                    OkMessageBox ok = new OkMessageBox("Thông báo", "Xóa thành công");
                    ok.ShowDialog();
                    SoLuongThuocDangChon = Const.ListThuocTemp.Count;
                }
            }
        }
        void LoadData()
        {
            if (ListThuocView == null)
            {
                ListThuocView = new ObservableCollection<ThuocDTO>();
            }
            // Add items from ListThuoc to ListThuocView
            if (Const.ListThuoc == null)
                return;
            else
            {
                foreach (var thuocDTO in Const.ListThuoc)
                {
                    ListThuocView.Add(thuocDTO);
                }
                Const.ListThuoc.Clear();
                Const.ListThuocTemp = new ObservableCollection<ThuocDTO>(ListThuocView);
            }
            ListThuocView = Const.ListThuocTemp;
            SoLuongThuocDangChon = ListThuocView.Count;


        }
        //Hàm lưu 
        private void Save(object obj)
        {
            if (obj != null)
            {
                if(obj is BenhNhanDangKhamView p)
                {
                    if (p.TenBN.Text == "")
                    {
                        OkMessageBox mb = new OkMessageBox("Thông báo", "Chưa chọn bệnh nhân để khám");
                        mb.ShowDialog();
                    }
                    else if (p.LoaiBenh.Text == "" || p.TrieuChung.Text == "")
                    {
                        OkMessageBox mb = new OkMessageBox("Chưa đủ thông tin!", "Chưa đủ thông tin để khám");
                        mb.ShowDialog();
                    }
                    else
                    {
                        // tạo hóa đơn
                        HOADON hd = new HOADON();
                        hd.TienThuoc = 0;
                        //Lưu phiếu khám bệnh
                        PHIEUKHAMBENH pkb = new PHIEUKHAMBENH();
                        pkb.TrieuChung = p.TrieuChung.Text;
                        pkb.NgayKham = DateTime.UtcNow.Date;
                        pkb.MaBN = int.Parse(p.MaBN.Text);
                        pkb.MaBS = IDBacSi;
                        pkb.MaLoaiBenh = SelectedLoaiBenh.MaLoaiBenh;
                        
                        //Lưu phiếu khám bệnh
                        DataProvider.Ins.DB.PHIEUKHAMBENHs.Add(pkb);
                        DataProvider.Ins.DB.SaveChanges();

                        hd.MaPKB = pkb.MaPKB;
                        hd.MaBN = pkb.MaBN;
                        //Lưu các chi tiết hóa đơn
                        if (ListThuocView == null)
                        {
                            
                        }
                        else
                        {
                            foreach (var thuoc in ListThuocView)
                            {
                                CT_PKB chitietpkb = new CT_PKB();
                                chitietpkb.MaPKB = pkb.MaPKB;
                                chitietpkb.SoLuong = thuoc.SL;
                                chitietpkb.MaThuoc = int.Parse(thuoc.MaThuoc);
                                //Lưu các chi tiết hóa đơn
                                DataProvider.Ins.DB.CT_PKB.Add(chitietpkb);
                                DataProvider.Ins.DB.SaveChanges();
                                //Cập nhật số lượng thuốc trong kho
                                foreach (var thuoctrongkho in listMed)
                                {
                                    if (thuoctrongkho.MaThuoc == chitietpkb.MaThuoc)
                                    {
                                        thuoctrongkho.SoLuong = thuoctrongkho.SoLuong - chitietpkb.SoLuong;
                                        //DataProvider.Ins.DB.SaveChanges();
                                        hd.TienThuoc = hd.TienThuoc + chitietpkb.SoLuong * thuoctrongkho.DonGiaBan;
                                        DataProvider.Ins.DB.SaveChanges();
                                        break;
                                    }
                                }

                            }
                        }
                       
                        THAMSO t = DataProvider.Ins.DB.THAMSOes.SingleOrDefault(h => h.MaThamSo == 2);
                        hd.TienKham = t.GiaTri;
                        hd.TongTien = hd.TienKham + (hd.TienThuoc ?? 0);
                        hd.TrangThai = "0";
                        //cập nhật trạng thái cho bệnh nhân
                        int MaBenhNhan = int.Parse(p.MaBN.Text);
                        BENHNHAN bn = DataProvider.Ins.DB.BENHNHANs.SingleOrDefault(h => h.MaBN == MaBenhNhan);
                        bn.TrangThai = true;
                        //Lưu hóa đơn
                        DataProvider.Ins.DB.HOADONs.Add(hd);
                        DataProvider.Ins.DB.SaveChanges();
                        OkMessageBox mb = new OkMessageBox("Thông báo", "Đã khám xong!");
                        mb.ShowDialog();
                        MessageBoxResult printResult = System.Windows.MessageBox.Show("Bạn có muốn in phiếu khám bệnh không?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (printResult == MessageBoxResult.Yes)
                        {
                            BenhNhanDangKhamView pkbtoPrint = new BenhNhanDangKhamView();
                            pkbtoPrint.GioiTinh.Text = p.GioiTinh.Text;
                            pkbtoPrint.ListViewMed.ItemsSource = p.ListViewMed.ItemsSource;
                            pkbtoPrint.LoaiBenh.ItemsSource = p.LoaiBenh.ItemsSource;
                            pkbtoPrint.LoaiBenh.SelectedItem = p.LoaiBenh.SelectedItem;
                            pkbtoPrint.LoaiBenh.SelectedIndex = p.LoaiBenh.SelectedIndex;


                            pkbtoPrint.MaBN.Text = p.MaBN.Text;
                            pkbtoPrint.MaBNformat.Text = p.MaBNformat.Text;
                            pkbtoPrint.NgKham.Text = p.NgKham.Text;
                            pkbtoPrint.NgsinhBN.Text = p.NgsinhBN.Text;
                            pkbtoPrint.TenBN.Text = p.TenBN.Text;
                            pkbtoPrint.TenBS.Text = p.TenBS.Text;
                            pkbtoPrint.TrieuChung.Text = p.TrieuChung.Text.ToString();
                            pkbtoPrint.Tuoi.Text = p.Tuoi.Text;
                            pkbtoPrint.TongSoThuoc.Text = p.TongSoThuoc.Text;
                            pkbtoPrint.saveBtn.Visibility = Visibility.Hidden;
                            pkbtoPrint.AddBtn.Visibility = Visibility.Hidden;



                            try
                            {
                                pkbtoPrint.IsEnabled = false;
                                PrintDialog printDialog = new PrintDialog();
                                if (printDialog.ShowDialog() == true)
                                {
                                    pkbtoPrint.LoaiBenh.SelectedItem = p.LoaiBenh.SelectedItem;
                                    pkbtoPrint.LoaiBenh.SelectedIndex = p.LoaiBenh.SelectedIndex;
                                    pkbtoPrint.TrieuChung.Text = p.TrieuChung.Text.ToString();
                                    printDialog.PrintVisual(pkbtoPrint.BenhNhanDangKhamUC, "Phiếu khám bệnh");
                                }
                            }
                            finally
                            {
                                pkbtoPrint.IsEnabled = true;
                            }

                            // Gọi hàm in hóa đơn và truyền đối tượng hoadonToPrint
                            //InHoaDon(pkbtoPrint);
                        }

                        // Clear the input fields
                        p.MaBNformat.Text = string.Empty;
                        p.TrieuChung.Text = string.Empty;
                        p.TenBN.Text = string.Empty;
                        p.NgsinhBN.Text = string.Empty;
                        p.Tuoi.Text = string.Empty;
                        p.GioiTinh.Text = string.Empty;
                        p.LoaiBenh.SelectedIndex = -1;

                        if (ListThuocView != null)
                        {
                            ListThuocView.Clear();
                        }
                        SoLuongThuocDangChon = 0;
                    }
                    
                }
            }
        }
        //void InHoaDon(BenhNhanDangKhamView parameter)
        //{
        //    try
        //    {
        //        //parameter.IsEnabled = false;
        //        PrintDialog printDialog = new PrintDialog();
        //        if (printDialog.ShowDialog() == true)
        //        {
        //            parameter.LoaiBenh.SelectedItem = 
        //            printDialog.PrintVisual(parameter, "Phiếu khám bệnh");
        //        }
        //    }
        //    finally
        //    {
        //        parameter.IsEnabled = true;
        //    }
        //}
        private void ThongTinND()
        {
            string tendangnhap = Const.TenDangNhap;
            User = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == tendangnhap).FirstOrDefault();
            string MaBS = User.MaBS.ToString();
            ObservableCollection<BACSI> DSBS = new ObservableCollection<BACSI>(DataProvider.Ins.DB.BACSIs);
            foreach ( var bacsi  in DSBS )
            {
                if (bacsi.MaBS.ToString() == MaBS) 
                {
                    Hoten = bacsi.HoTen;
                    IDBacSi = bacsi.MaBS;
                    break;
                }
            }
            
        }

    }
}
