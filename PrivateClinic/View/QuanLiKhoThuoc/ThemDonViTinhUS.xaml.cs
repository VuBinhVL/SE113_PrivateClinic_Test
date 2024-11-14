using PrivateClinic.ViewModel.QuanLiKhoThuocVM;
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

namespace PrivateClinic.View.QuanLiKhoThuoc
{
    /// <summary>
    /// Interaction logic for ThemDonViTinhUS.xaml
    /// </summary>
    public partial class ThemDonViTinhUS : UserControl
    {
        public ThemDonViTinhUS()
        {
            InitializeComponent();
            ThemDonViTinhViewModel viewModel = new ThemDonViTinhViewModel();
            this.DataContext = viewModel;
        }
    }
}
