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
using System.Windows.Shapes;

namespace PrivateClinic.View.HoSoBacSi
{
    /// <summary>
    /// Interaction logic for ThemBacSiView.xaml
    /// </summary>
    public partial class ThemBacSiView : Window
    {
        public ThemBacSiView()
        {
            InitializeComponent();
            ThemBacSiViewModel viewModel = new ThemBacSiViewModel(this);
            this.DataContext = viewModel;
        }
    }
}
