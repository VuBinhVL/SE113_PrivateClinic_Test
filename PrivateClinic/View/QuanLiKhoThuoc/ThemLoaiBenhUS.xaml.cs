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
    /// Interaction logic for ThemLoaiBenhUS.xaml
    /// </summary>
    public partial class ThemLoaiBenhUS : UserControl
    {
        public ThemLoaiBenhUS()
        {
            InitializeComponent();
            ThemLoaiBenhViewMiodel viewModel =new ThemLoaiBenhViewMiodel();
            this.DataContext = viewModel;
        }
    }
}
