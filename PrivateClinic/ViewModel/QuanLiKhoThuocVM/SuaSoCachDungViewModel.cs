using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateClinic.View.QuanLiKhoThuoc;
using System.Windows.Controls;
using System.Windows.Input;
using PrivateClinic.ViewModel.OtherViewModels;

namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    public class SuaSoCachDungViewModel:BaseViewModel
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
        public SuaSoCachDungViewModel()
        {
            ExitCommand = new RelayCommand<SuaSoCachDungView>((p) => { return p == null ? false : true; }, (p) => _ExitCommand(p));
            SwitchViewCommand = new ViewModelCommand(SwitchView);
        }
        private void SwitchView(object userControlName)
        {
            string userControlNameStr = userControlName as string;
            switch (userControlNameStr)
            {
                case "Thuoccu":

                    CurrentView = new ThemCachDungUS();
                    break;
                case "Thuocmoi":
                    CurrentView = new XoaCachDungUS();
                    break;
            }
        }

        private void _ExitCommand(SuaSoCachDungView view)
        {
            view.Close();
        }
    }
}
