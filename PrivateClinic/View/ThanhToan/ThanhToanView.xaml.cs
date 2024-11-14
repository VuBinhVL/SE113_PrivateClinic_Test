using PrivateClinic.Model;
using PrivateClinic.ViewModel.QuanLiTiepDon;
using PrivateClinic.ViewModel.ThanhToan;
using System;
using System.Collections.Generic;
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

namespace PrivateClinic.View.ThanhToan
{
    /// <summary>
    /// Interaction logic for ThanhToanView.xaml
    /// </summary>
    public partial class ThanhToanView : Page
    {
        public ThanhToanView()
        {
            InitializeComponent();
            ThanhToanViewModel viewmodel = new ThanhToanViewModel();
            this.DataContext = viewmodel;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ThanhToanViewModel viewModel)
            {
                var selectedHoaDon = (sender as Button)?.CommandParameter as HOADON;

                if (selectedHoaDon != null)
                {
                    viewModel.DeleteCommand.Execute(selectedHoaDon);
                    ListViewHD.Items.Refresh();
                }
            }
        }

        private void PaymentHDButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ThanhToanViewModel viewModel)
            {
                var selectedHoaDon = (sender as Button)?.CommandParameter as HOADON;

                if (selectedHoaDon != null)
                {
                    viewModel.PayHDCommand.Execute(selectedHoaDon);

                }
            }
        }
    }
}
