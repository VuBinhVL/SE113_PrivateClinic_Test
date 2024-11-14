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
    /// Interaction logic for XoaDonViTinhUS.xaml
    /// </summary>
    public partial class XoaDonViTinhUS : UserControl
    {
        public XoaDonViTinhUS()
        {
            InitializeComponent();
            XoaDonViTinhViewModel viewModel = new XoaDonViTinhViewModel();
            this.DataContext = viewModel;
            viewModel.LoadCommand.Execute(null);
            dvtcbx.ItemsSource = viewModel.TenDVTs;
        }
    }
}
