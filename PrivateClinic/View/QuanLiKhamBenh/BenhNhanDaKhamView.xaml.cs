using PrivateClinic.ViewModel.QuanLiKhamBenhVM;
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

namespace PrivateClinic.View.QuanLiKhamBenh
{
    /// <summary>
    /// Interaction logic for BenhNhanDaKhamView.xaml
    /// </summary>
    public partial class BenhNhanDaKhamView : UserControl
    {
        public BenhNhanDaKhamView()
        {
            InitializeComponent();
            BenhNhanDaKhamViewModel viewModel = new BenhNhanDaKhamViewModel();
            this.DataContext = viewModel;
        }
    }
}
