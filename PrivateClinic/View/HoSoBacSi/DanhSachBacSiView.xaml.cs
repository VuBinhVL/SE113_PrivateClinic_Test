using PrivateClinic.Model;
using PrivateClinic.ViewModel.HoSoBacSiVM;
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

namespace PrivateClinic.View.HoSoBacSi
{
    /// <summary>
    /// Interaction logic for DanhSachBacSiView.xaml
    /// </summary>
    public partial class DanhSachBacSiView : UserControl
    {
        public DanhSachBacSiView()
        {
            InitializeComponent();
            DanhSachBacSiViewModel viewModel = new DanhSachBacSiViewModel();
            this.DataContext = viewModel;
        }

       
        

        
    }
}
