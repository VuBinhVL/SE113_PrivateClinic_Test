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
using PrivateClinic.ViewModel.OtherViewModels;

namespace PrivateClinic.ViewModel.HoSoBacSiVM
{
    public class ThemNhomNguoiDungViewModel:BaseViewModel
    {
        public ICommand ExitCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        private ObservableCollection<NHOMNGUOIDUNG> _nhomND;

        public ObservableCollection<NHOMNGUOIDUNG> nhomND
        {
            get => _nhomND;
            set { _nhomND = value; OnPropertyChanged(nameof(nhomND)); }

        }
        public ThemNhomNguoiDungViewModel()
        {
            ExitCommand = new RelayCommand<ThemNhomNguoiDungView>((p) => true, (p) => Close(p));
            SaveCommand = new RelayCommand<ThemNhomNguoiDungView>((p) => true, (p) => _saveCommand(p));
        }
        void Close(ThemNhomNguoiDungView p)
        {
            p.Close();
        }
        private void _saveCommand(ThemNhomNguoiDungView p)
        {
            nhomND = new ObservableCollection<NHOMNGUOIDUNG>(DataProvider.Ins.DB.NHOMNGUOIDUNGs);
            NHOMNGUOIDUNG nHOMNGUOIDUNG = new NHOMNGUOIDUNG();
            {
                nHOMNGUOIDUNG.TenNhom = p.NhomNDmoi.Text;
            }
            DataProvider.Ins.DB.NHOMNGUOIDUNGs.Add(nHOMNGUOIDUNG);
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Thêm nhóm người dùng mới thành công!", "THÔNG BÁO");
            p.Close();

        }
    }
}
