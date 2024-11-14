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
using PrivateClinic.ViewModel.HoSoBacSiVM;
namespace PrivateClinic.View.HoSoBacSi
{
    /// <summary>
    /// Interaction logic for ThemNhomNguoiDungView.xaml
    /// </summary>
    public partial class ThemNhomNguoiDungView : Window
    {
        public ThemNhomNguoiDungView()
        {
            InitializeComponent();
            ThemNhomNguoiDungViewModel viewModel = new ThemNhomNguoiDungViewModel();
            this.DataContext = viewModel;
        }
    }
}
