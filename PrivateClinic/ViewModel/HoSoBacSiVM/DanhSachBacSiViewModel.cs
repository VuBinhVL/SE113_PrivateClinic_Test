using PrivateClinic.Model;
using PrivateClinic.View.HoSoBacSi;
using PrivateClinic.View.MessageBox;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PrivateClinic.ViewModel.HoSoBacSiVM
{
    public class DanhSachBacSiViewModel: BaseViewModel
    {
        #region Các Command và Property
        private ObservableCollection<BACSI> dsbs;
        public ObservableCollection<BACSI> DSBS
        {
            get => dsbs;
            set
            {
                dsbs = value;
                OnPropertyChanged(nameof(DSBS));
                SoLuong = DSBS.Count;

            }
        }

        private ObservableCollection<BACSI> filterDSBS;
        public ObservableCollection<BACSI> FilterDSBS
        {
            get => filterDSBS;
            set
            {
                if (filterDSBS != value)
                {
                    filterDSBS = value;
                    OnPropertyChanged(nameof(FilterDSBS));

                }
            }
        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    FilterDSBacSi();
                }
            }
        }
        public ICommand ShowWDAddDoctor { get; set; }
        public ICommand EditDoctorCommand { get; set; }
        public ICommand DeleteDoctorCommand { get; set; }
        public ICommand DoctorDetailCommand { get; set; }

        private int soLuong;
        public int SoLuong
        {
            get => soLuong;
            set
            {
                if (soLuong != value)
                {
                    soLuong = value;
                    OnPropertyChanged(nameof(SoLuong));
                }
            }
        }
        #endregion

        //Hàm khởi tạo
        public DanhSachBacSiViewModel()
        {
            LoadData();
            AddDoctor();
            EditDoctor();
            DoctorDetailF();
            DeleteDoctorCommand = new RelayCommand<BACSI>((p) => p != null, DeleteBacSi);

        }
        #region Chức năng tìm kiếm

    
        void SearchBS()
        {
            FilterDSBS = new ObservableCollection<BACSI>(DSBS);//ban đầu thì không cần lọc
        }
        private void FilterDSBacSi()
        {
            // Cập nhật FilteredDSBS dựa trên SearchText
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilterDSBS = new ObservableCollection<BACSI>(DSBS);
            }
            else
            {
                FilterDSBS = new ObservableCollection<BACSI>(
                    DSBS.Where(s => s.HoTen.ToLower().Contains(SearchText.ToLower())));
            }
        }
        #endregion

        #region Chức năng hiển thị danh sách bác sĩ
        void LoadData()
        {
            DSBS = new ObservableCollection<BACSI>(DataProvider.Ins.DB.BACSIs);
            FormatMaBS();
            SearchBS();
        }
        #endregion

        #region Chức năng thêm 1 bác sĩ
        private void ShowWDDoctor(object obj)
        {
            ThemBacSiView view = new ThemBacSiView();
            view.ShowDialog();
            LoadData();
        }

        void AddDoctor()
        {
            ShowWDAddDoctor = new ViewModelCommand(ShowWDDoctor);
        }
        #endregion

        #region Chức năng sửa 1 bác sĩ
        void EditDoctor()
        {
            EditDoctorCommand = new ViewModelCommand(ShowWDEditDoctor);
        }
        private void ShowWDEditDoctor(object obj)
        {
            SuaThongTinBacSiView view = new SuaThongTinBacSiView((BACSI) obj);
            view.ShowDialog();
            LoadData();
        }

        #endregion

        #region Chức năng xóa thông tin 1 bác sĩ
        void DeleteBacSi(BACSI selectedItem)
        {
            YesNoMessageBox r = new YesNoMessageBox("Thông báo","Bạn muốn xóa bệnh nhân này không ?");
            if (r.DialogResult == true)  
            {
                if (selectedItem != null && selectedItem.MaBS != 1) 
                {
                    //Xóa tài khoàn trước
                    var taikhoan = DataProvider.Ins.DB.NGUOIDUNGs.Where(h => h.MaBS == selectedItem.MaBS).ToList();
                    DataProvider.Ins.DB.NGUOIDUNGs.RemoveRange(taikhoan);
                    //Cập nhật mã bác sĩ trong các phiếu khám bệnh bằng NULL
                    var phieukhambenh = DataProvider.Ins.DB.PHIEUKHAMBENHs.Where(h => h.MaBS == selectedItem.MaBS).ToList();
                    foreach (var h in phieukhambenh) 
                        h.MaBS = null;
                    //Xóa bác sĩ
                    DataProvider.Ins.DB.BACSIs.Remove(selectedItem);

                    //Lưu xuống CSDL
                    DataProvider.Ins.DB.SaveChanges();

                    //Cập nhật listview
                    FilterDSBS.Remove(selectedItem);
                    SoLuong = FilterDSBS.Count;
                    OkMessageBox mb = new OkMessageBox("Thông báo", "Xóa thành công");
                    mb.ShowDialog();
                }
                else
                {
                    OkMessageBox mb = new OkMessageBox("Thông báo", "Không thề xóa admin");
                    mb.ShowDialog();
                }
            }
        }
        #endregion

        #region Chức năng double click hiển thị thông tin
        void DoctorDetailF()
        {
            DoctorDetailCommand = new ViewModelCommand(DoctorDetail);
        }

        private void DoctorDetail(object obj)
        {
            BACSI bs = (BACSI)obj;
            ThongTinChiTietCuaMotBacSIView doctordetailView = new ThongTinChiTietCuaMotBacSIView(bs);
            doctordetailView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            doctordetailView.ShowDialog();
            LoadData();


        }
        #endregion

        #region Chức năng điều chỉnh mã bác sĩ lên view
        void FormatMaBS()
        {
            foreach (var item in DSBS)
            {
                if (item.MaBS < 10)
                {
                    item.formatMaBS = "BS00" + item.MaBS;
                }
                else if (item.MaBS < 100)
                {
                    item.formatMaBS = "BS0" + item.MaBS;
                }
                else
                    item.formatMaBS = "BS" + item.MaBS;
            }
        }
        #endregion
    }
}
