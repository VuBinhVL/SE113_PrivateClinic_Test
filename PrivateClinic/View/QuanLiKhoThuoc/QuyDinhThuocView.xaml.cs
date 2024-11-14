using PrivateClinic.ViewModel.QuanLiKhoThuocVM;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for QuyDinhThuocView.xaml
    /// </summary>
    public partial class QuyDinhThuocView : UserControl
    {
        public QuyDinhThuocView()
        {
            InitializeComponent();
            QuyDinhThuocViewModel viewModel = new QuyDinhThuocViewModel();
            this.DataContext = viewModel;
            //viewModel.LoadCommand.Execute(null);
            //cachdung.Text = string.Join(", ", viewModel.CachDung);
            //Tilegia.Text = string.Join(", ", viewModel.Tilegia);          
            //tienkham.Text = string.Join(", ", viewModel.tienkham.Select(x => string.Format("{0:0,0}", x)));
            //dvt.Text = string.Join(", ", viewModel.dvt);
            //loaibenh.Text = string.Join(", ", viewModel.loaibenh);
            //loaithuoc.Text = string.Join(", ", viewModel.loaithuoc);
        }
    }
}
