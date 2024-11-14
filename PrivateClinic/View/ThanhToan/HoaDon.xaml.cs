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


namespace PrivateClinic.View.ThanhToan
{
    /// <summary>
    /// Interaction logic for HoaDonView.xaml
    /// </summary>
    public partial class HoaDonView : Window
    {
        public HoaDonView(HoaDonViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
