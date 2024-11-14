using PrivateClinic.Model;
using PrivateClinic.ViewModel.OtherViewModels;
using PrivateClinic.View.ThanhToan;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Resources;
using System;
using PrivateClinic.View.MessageBox;

namespace PrivateClinic.ViewModel.ThanhToan
{
    public class ThanhToanViewModel : BaseViewModel
    {
        #region Properties
        private ObservableCollection<HOADON> _listHD;
        public ObservableCollection<HOADON> listHD { get => _listHD; set { 
                _listHD = value;
                OnPropertyChanged();
                UpdateUnpaidInvoiceCount();
            }}

        private PaymentStatus _selectedPaymentStatus = PaymentStatus.All;
        public PaymentStatus SelectedPaymentStatus
        {
            get => _selectedPaymentStatus;
            set
            {
                _selectedPaymentStatus = value;
                OnPropertyChanged(nameof(SelectedPaymentStatus));
                FilterListHD();
            }
        }

        private HOADON _hoaDon;
        public HOADON HoaDon
        {
            get => _hoaDon;
            set
            {
                _hoaDon = value;
                OnPropertyChanged(nameof(HoaDon));
            }
        }

        private int _unpaidInvoiceCount;
        public int UnpaidInvoiceCount
        {
            get => _unpaidInvoiceCount;
            set
            {
                _unpaidInvoiceCount = value;
                OnPropertyChanged(nameof(UnpaidInvoiceCount));
            }
        }

        public HoaDonView hoaDonView { get; set; }
        public ICommand SearchCommand { get; set; }


        public ICommand LoadCommand { get; set; }
        public ICommand LoadSLHDCommand { get; set; }

        //LISTVIEW COMMAND
        public ICommand PayHDCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public enum PaymentStatus
        {
            All = -1,
            Unpaid = 0,
            Paid = 1
        }
        private string chucvu;
        public string ChucVu
        {
            get => chucvu;
            set
            {
                chucvu = value;
                OnPropertyChanged(nameof(ChucVu));
            }
        }
        private string tenNV;
        public string TenNV
        {
            get => tenNV;
            set
            {
                tenNV = value;
                OnPropertyChanged(nameof(TenNV));
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
        private BitmapImage imageSource;
        public BitmapImage ImageSource
        {
            get => imageSource;
            set
            {
                imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }
        #endregion

        public ThanhToanViewModel()
        {
            ThongTinND();
            listHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            SearchCommand = new RelayCommand<ThanhToanView>((p) => { return p == null ? false : true; }, (p) => _SearchCommand(p));
            PayHDCommand = new RelayCommand<HOADON>((p) => { return p == null ? false : true; }, (p) => _PayHDCommand(p));
            DeleteCommand = new RelayCommand<HOADON>((p) => { return p == null ? false : true; }, (p) => _DeleteCommand(p));
        }

        #region Functions
        private void UpdateUnpaidInvoiceCount()
        {
            var listHDFull = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            UnpaidInvoiceCount = listHDFull.Count(hd => hd.TrangThai == "0");
            OnPropertyChanged(nameof(UnpaidInvoiceCount));
        }


        void _SearchCommand(ThanhToanView parameter)
        {
            ObservableCollection<HOADON> temp = new ObservableCollection<HOADON>();
            if (!string.IsNullOrEmpty(parameter.txbSearch.Text))
            {
                string searchKeyword = parameter.txbSearch.Text.ToLower();
                temp = new ObservableCollection<HOADON>(listHD.Where(s => s.BENHNHAN.HoTen.ToLower().Contains(searchKeyword)));
                listHD = temp;
            }
            else
            {
                FilterListHD();
            }
        }

        void _DeleteCommand(HOADON selectedItem)
        {
            YesNoMessageBox h = new YesNoMessageBox("THÔNG BÁO", "Bạn có muốn xóa hóa đơn này ?");
            h.ShowDialog();
            if (h.DialogResult == true)
            {
                if (selectedItem != null)
                {
                    var relatedCTBCDTs = DataProvider.Ins.DB.CT_BCDT.Where(ct => ct.SoHD == selectedItem.SoHD).ToList();
                    DataProvider.Ins.DB.CT_BCDT.RemoveRange(relatedCTBCDTs);

                    var relatedCT_PKBs = DataProvider.Ins.DB.CT_PKB.Where(ctpkb => ctpkb.MaPKB == selectedItem.MaPKB).ToList();
                    DataProvider.Ins.DB.CT_PKB.RemoveRange(relatedCT_PKBs);

                    var relatedPHIEUKBs = DataProvider.Ins.DB.PHIEUKHAMBENHs.Where(pkb => pkb.MaPKB == selectedItem.MaPKB).ToList();
                    DataProvider.Ins.DB.PHIEUKHAMBENHs.RemoveRange(relatedPHIEUKBs);

                    DataProvider.Ins.DB.HOADONs.Remove(selectedItem);

                    DataProvider.Ins.DB.SaveChanges();

                    listHD.Remove(selectedItem);
                    UpdateUnpaidInvoiceCount();
                    RefreshData();
                }
            }
        }

        public void SetThanhToanHDView(HoaDonView view)
        {
            hoaDonView = view;
        }

        private void FilterListHD()
        {
            var allHoaDons = DataProvider.Ins.DB.HOADONs.ToList();
            if (SelectedPaymentStatus == PaymentStatus.All)
            {
                listHD = new ObservableCollection<HOADON>(allHoaDons);
            }
            else
            {
                var statusString = ((int)SelectedPaymentStatus).ToString();
                listHD = new ObservableCollection<HOADON>(
                    allHoaDons.Where(x => x.TrangThai == statusString));
            }
        }
        
        public void RefreshData()
        {
            listHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs); // Refresh the list
            OnPropertyChanged(nameof(listHD));
            SelectedPaymentStatus = PaymentStatus.All;
        }

        void _PayHDCommand(HOADON selectedItem)
        {
            if (selectedItem != null)
            {
                HoaDonViewModel hoaDonVM = new HoaDonViewModel();
                hoaDonVM.CurrentHoaDon = selectedItem;

                HoaDonView hoaDonView = new HoaDonView(hoaDonVM);
                hoaDonView.DataContext = hoaDonVM;
                double mainWindowRightEdge = Application.Current.MainWindow.Left + Application.Current.MainWindow.Width;
                hoaDonView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                hoaDonView.Height = 550;
                hoaDonView.ShowDialog();

                if (hoaDonView.DialogResult == true)
                {
                    this.RefreshData();
                }
            }
        }
        //Hàm load thông tin người dùng và ảnh
        private void ThongTinND()
        {
            string tendangnhap = Const.TenDangNhap;
            User = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == tendangnhap).FirstOrDefault();
            string MaBS = User.MaBS.ToString();
            ObservableCollection<BACSI> DSBS = new ObservableCollection<BACSI>(DataProvider.Ins.DB.BACSIs);
            if (Const.PQ.MaNhom == 1)
                ChucVu = "Admin";
            else
                ChucVu = "Nhân viên";
            foreach (BACSI bs in DSBS)
            {
                if (bs.MaBS.ToString() == MaBS)
                {
                    TenNV = bs.HoTen;
                    if (bs.Image == null)
                    {
                        Uri resourceUri = new Uri("pack://application:,,,/ResourceXAML/image/ic_male_user_blue.png", UriKind.Absolute);
                        StreamResourceInfo streamInfo = System.Windows.Application.GetResourceStream(resourceUri);
                        byte[] imageBytes;
                        using (Stream imageStream = streamInfo.Stream)
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                imageStream.CopyTo(ms);
                                imageBytes = ms.ToArray();
                            }
                        }
                        ImageSource = ImageViewModel.ByteArrayToBitmapImage(imageBytes);
                    }
                    else
                    {
                        BitmapImage image = ImageViewModel.ByteArrayToBitmapImage(bs.Image);
                        ImageSource = image;
                    }
                    break;
                }
            }
        }
        #endregion
    }
}


