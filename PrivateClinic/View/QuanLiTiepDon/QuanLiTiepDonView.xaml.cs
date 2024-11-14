using PrivateClinic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MaterialDesignThemes.Wpf.Theme;
using Button = System.Windows.Controls.Button;
using PrivateClinic.ViewModel.QuanLiTiepDon;
using PrivateClinic.ViewModel.HoSoBacSiVM;
using PrivateClinic.View.QuanLiKhamBenh;

namespace PrivateClinic.View.QuanLiTiepDon
{
    /// <summary>
    /// Interaction logic for QuanLiTiepDonView.xaml
    /// </summary>
    public partial class QuanLiTiepDonView : UserControl
    {
        public QuanLiTiepDonView()
        {
            InitializeComponent();
            QuanLiTiepDonViewModel viewmodel = new QuanLiTiepDonViewModel();
            this.DataContext = viewmodel;
            viewmodel.LoadCommand.Execute(null);

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is QuanLiTiepDonViewModel viewModel)
            {
                var selectedBenhNhan = (sender as Button)?.CommandParameter as BENHNHAN;

                if (selectedBenhNhan != null)
                {
                    viewModel.DeleteCommand.Execute(selectedBenhNhan);
                    ListViewBN.Items.Refresh();
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is QuanLiTiepDonViewModel viewModel)
            {
                var selectedBenhNhan= (sender as System.Windows.Controls.Button)?.CommandParameter as BENHNHAN;

                if (selectedBenhNhan != null)
                {
                    viewModel.EditBNCommand.Execute(selectedBenhNhan);

                }
            }
        }
        private void KhamBNButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = new BenhNhanDangKhamViewModel(); // Tạo ViewModel cho UserControl
            var userControl = new BenhNhanDangKhamView(); // Tạo UserControl
            userControl.DataContext = viewModel; // Gán ViewModel cho UserControl
            var selectedBenhNhan = (sender as Button)?.CommandParameter as BENHNHAN;

            // Tìm kiếm QuanLiKhamBenhView hoặc Window chứa UserControl hiện tại
            var parent = FindParent<QuanLiKhamBenhView>(this);

            if (parent != null && parent.DataContext is QuanLiKhamBenhViewModel parentViewModel)
            {
                // Thiết lập CurrentView trong QuanLiKhamBenhViewModel thành UserControl
                parentViewModel.CurrentView = userControl;
                if (selectedBenhNhan != null)
                {
                    userControl.SetData(selectedBenhNhan);
                }

            }

            
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
                return null;

            if (parentObject is T parent)
                return parent;

            return FindParent<T>(parentObject);
        }


    }
}
