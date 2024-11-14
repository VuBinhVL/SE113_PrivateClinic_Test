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
    /// Interaction logic for XoaCachDungUS.xaml
    /// </summary>
    public partial class XoaCachDungUS : UserControl
    {
        public XoaCachDungUS()
        {
            InitializeComponent();
            XoaCachDungViewModel viewModel = new XoaCachDungViewModel();
            this.DataContext = viewModel;
            viewModel.LoadCommand.Execute(null);
            dvtcbx.ItemsSource = viewModel.TenDVTs;
        }
    }
}
