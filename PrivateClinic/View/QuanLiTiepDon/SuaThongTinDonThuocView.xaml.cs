using PrivateClinic.Model;
using PrivateClinic.ViewModel.QuanLiTiepDon;
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

namespace PrivateClinic.View.QuanLiTiepDon
{
    /// <summary>
    /// Interaction logic for SuaThongTinDonThuocView.xaml
    /// </summary>
    public partial class SuaThongTinDonThuocView : Window
    {
        public SuaThongTinDonThuocView(ThuocDTO thuoc)
        {
            InitializeComponent();
            SuaThongTinDonThuocViewModel viewModel = new SuaThongTinDonThuocViewModel(this);
            viewModel.thuocDTO = thuoc;
            viewModel.LoadEditCurrent();
            this.DataContext = viewModel;
        }
    }
}
