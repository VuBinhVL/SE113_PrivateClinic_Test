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
using System.Windows.Shapes;

namespace PrivateClinic.View.HoSoBacSi
{
    /// <summary>
    /// Interaction logic for SuaThongTinBacSiView.xaml
    /// </summary>
    public partial class SuaThongTinBacSiView : Window
    {
        public SuaThongTinBacSiView(BACSI bacsi)
        {
            InitializeComponent();
            SuaThongTinBacSiViewModel viewModel = new SuaThongTinBacSiViewModel(this);
            viewModel.bacsi = bacsi;
            viewModel.loadEditCurrent();
            this.DataContext = viewModel;
        }
    }
}
