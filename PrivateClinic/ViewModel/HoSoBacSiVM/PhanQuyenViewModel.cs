using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PrivateClinic.Model;
using PrivateClinic.View.HoSoBacSi;
using PrivateClinic.View.QuanLiKhoThuoc;
using PrivateClinic.ViewModel.OtherViewModels;
using PrivateClinic.ViewModel.QuanLiKhoThuocVM;
namespace PrivateClinic.ViewModel.HoSoBacSiVM
{
    public class PhanQuyenViewModel : BaseViewModel
    {

        private ObservableCollection<PQDTO> _listPQ;

        public ObservableCollection<PQDTO> listPQ
        {
            get => _listPQ;
            set
            {
                _listPQ = value; OnPropertyChanged(nameof(listPQ));
            }

        }
        private ObservableCollection<PHANQUYEN> _phanquyen;

        public ObservableCollection<PHANQUYEN> phanquyen
        {
            get => _phanquyen;
            set { _phanquyen = value; OnPropertyChanged(nameof(phanquyen)); }

        }
        private ObservableCollection<NHOMNGUOIDUNG> _nhomND;

        public ObservableCollection<NHOMNGUOIDUNG> nhomND
        {
            get => _nhomND;
            set { _nhomND = value; OnPropertyChanged(nameof(nhomND)); }

        }
        private ObservableCollection<CHUCNANG> _chucnang;

        public ObservableCollection<CHUCNANG> chucnang
        {
            get => _chucnang;
            set { _chucnang = value; OnPropertyChanged(nameof(chucnang)); }

        }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public PhanQuyenViewModel()
        {
            LoadData();
            AddCommand = new RelayCommand<PhanQuyenUS>((p) => { return p == null ? false : true; }, (p) => _AddCommand(p));
            EditCommand = new RelayCommand<PQDTO>((p) => { return p == null ? false : true; }, (p) => _EditCommand(p));
        }
        void LoadData()
        {
            phanquyen = new ObservableCollection<PHANQUYEN>(DataProvider.Ins.DB.PHANQUYENs);
            chucnang = new ObservableCollection<CHUCNANG>(DataProvider.Ins.DB.CHUCNANGs);
            nhomND = new ObservableCollection<NHOMNGUOIDUNG>(DataProvider.Ins.DB.NHOMNGUOIDUNGs);
            listPQ = new ObservableCollection<PQDTO>();
            foreach (var p in phanquyen)
            {
                foreach (var c in chucnang)
                {
                    if (c.MaChucNang == p.MaChucNang)
                    {
                        PQDTO newpq = new PQDTO()
                        {
                            ChucNang = c.TenChucNang
                        };
                        foreach (var nnd in nhomND)
                        {
                            if (nnd.MaNhom == p.MaNhom)
                            {
                                newpq.TenNhom = nnd.TenNhom;
                            }
                        }
                        listPQ.Add(newpq);
                        break;
                    }
                }
            }

        }
        private void _AddCommand(PhanQuyenUS p)
        {
            ThemNhomNguoiDungView a = new ThemNhomNguoiDungView();
            a.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            a.ShowDialog();
        }
        private void _EditCommand(PQDTO selectedItem)
        {
            if (selectedItem != null)
            {
                nhomND = new ObservableCollection<NHOMNGUOIDUNG>(DataProvider.Ins.DB.NHOMNGUOIDUNGs);
                SuaNhomNguoiDungViewModel.Instance.EditNhomNDView = new SuaNhomNguoiDungView();
                SuaNhomNguoiDungViewModel.Instance.EditNhomNDView.TenChucNang.Text = selectedItem.ChucNang.ToString();
                SuaNhomNguoiDungViewModel.Instance.EditNhomNDView.NhomNDcbx.ItemsSource = nhomND.Select(n => n.TenNhom);
                //SuaNhomNguoiDungViewModel.Instance.EditNhomNDView.NhomNDcbx.SelectedValue = selectedItem.TenNhom.ToString();
                SuaNhomNguoiDungViewModel.Instance.EditNhomNDView.NhomNDcbx.SelectedItem =
                SuaNhomNguoiDungViewModel.Instance.EditNhomNDView.NhomNDcbx.Items.Cast<string>().FirstOrDefault(item => item == selectedItem.TenNhom.ToString());

                double mainWindowRightEdge = Application.Current.MainWindow.Left + Application.Current.MainWindow.Width;
                SuaNhomNguoiDungViewModel.Instance.EditNhomNDView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                SuaNhomNguoiDungViewModel.Instance.EditNhomNDView.ShowDialog();
                LoadData();
            }
        }
    }
}
