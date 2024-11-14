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
using System.Windows.Shapes;

namespace PrivateClinic.View.QuanLiKhoThuoc
{
    /// <summary>
    /// Interaction logic for SuaSoCachDungView.xaml
    /// </summary>
    public partial class SuaSoCachDungView : Window
    {
        public SuaSoCachDungView()
        {
            InitializeComponent();
            Main.Content = new ThemCachDungUS();
            ThuocCuRBtn.IsChecked = true;
            SuaSoCachDungViewModel viewModel = new SuaSoCachDungViewModel();
            this.DataContext = viewModel;
        }
    }
}
