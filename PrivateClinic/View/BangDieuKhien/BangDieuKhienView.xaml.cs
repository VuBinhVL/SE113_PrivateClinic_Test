using PrivateClinic.Model;
using PrivateClinic.ViewModel.BangDieuKhien;
using PrivateClinic.ViewModel.ThanhToan;
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

namespace PrivateClinic.View.BangDieuKhien
{
    /// <summary>
    /// Interaction logic for BangDieuKhienView.xaml
    /// </summary>
    public partial class BangDieuKhienView : Page
    {
        public BangDieuKhienView()
        {
            InitializeComponent();
            BangDieuKhienViewModel viewmodel = new BangDieuKhienViewModel();
            this.DataContext = viewmodel;
        }
    }
}