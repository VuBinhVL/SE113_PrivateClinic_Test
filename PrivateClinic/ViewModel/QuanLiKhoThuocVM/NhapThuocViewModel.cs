using PrivateClinic.Model;
using PrivateClinic.View.QuanLiKhoThuoc;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    public class NhapThuocViewModel : BaseViewModel
    {
        public static NhapThuocViewModel Instance { get; } = new NhapThuocViewModel();
        public NhapThuocView AddThuocView { get; set; }
        public ICommand AddMedicineCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand quitCommand { get; set; }
        public ICommand SetAddThuocView { get; set; }
        public ICommand LoadCommand { get; set; }

        //private ThemThuocMoiView _themThuocMoiView;

        //// Constructor
        //public ThemThuocViewModel(ThemThuocMoiView wd)
        //{
        //    this._themThuocMoiView = wd;
        //    AddMedicineCommand = new RelayCommand<object>(CanExecuteAdd, ExecuteAdd);
        //    quitCommand = new ViewModelCommand(quit);
        //}
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
        public ICommand SwitchViewCommand { get; set; }
        public NhapThuocViewModel()
        {
     
            ExitCommand = new RelayCommand<NhapThuocView>((p) => { return p == null ? false : true; }, (p) => _ExitCommand(p));    
            SwitchViewCommand = new ViewModelCommand(SwitchView);
        }
        private void SwitchView(object userControlName)
        {
            string userControlNameStr = userControlName as string;
            switch (userControlNameStr)
            {
                case "Thuoccu":

                    CurrentView = new ThemThuocCuView();
                    break;
                case "Thuocmoi":
                    CurrentView = new ThemThuocMoiView();
                    break;
            }
        }

        private void _ExitCommand(NhapThuocView view)
        {
            view.Close();
        }











    }
}
