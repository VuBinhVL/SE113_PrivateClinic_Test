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
using PrivateClinic.ViewModel.QuanLiTiepDon;
namespace PrivateClinic.View.QuanLiTiepDon
{
    /// <summary>
    /// Interaction logic for ThemThuocChoBenhNhanView.xaml
    /// </summary>
    public partial class ThemThuocChoBenhNhanView : Window
    {
        public ThemThuocChoBenhNhanView()
        {
            InitializeComponent();
            ThemThuocChoBenhNhanViewModel viewModel= new ThemThuocChoBenhNhanViewModel(this);
            this.DataContext = viewModel;
        }
    }
}
