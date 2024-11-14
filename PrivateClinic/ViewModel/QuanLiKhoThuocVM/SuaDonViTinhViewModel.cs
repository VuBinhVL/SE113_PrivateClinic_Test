using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using PrivateClinic.View.QuanLiKhoThuoc;
using PrivateClinic.ViewModel.OtherViewModels;

namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    public class SuaDonViTinhViewModel:BaseViewModel
    {
        public ICommand ExitCommand { get; set; }
        public ICommand SwitchViewCommand { get; set; }
        private UserControl currentView;
        public UserControl CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }
        public SuaDonViTinhViewModel()
        {
            ExitCommand = new RelayCommand<SuaDonViTinhView>((p) => { return p == null ? false : true; }, (p) => _ExitCommand(p));
            SwitchViewCommand = new ViewModelCommand(SwitchView);
        }
        private void SwitchView(object userControlName)
        {
            string userControlNameStr = userControlName as string;
            switch (userControlNameStr)
            {
                case "Thuoccu":

                    CurrentView = new ThemDonViTinhUS();
                    break;
                case "Thuocmoi":
                    CurrentView = new XoaDonViTinhUS();
                    break;
            }
        }

        private void _ExitCommand(SuaDonViTinhView view)
        {
            view.Close();
        }

    }
}
