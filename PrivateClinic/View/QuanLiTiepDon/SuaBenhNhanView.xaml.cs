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
using PrivateClinic.Model;
using PrivateClinic.ViewModel.QuanLiTiepDon;

namespace PrivateClinic.View.QuanLiTiepDon
{
    /// <summary>
    /// Interaction logic for SuaBenhNhanView.xaml
    /// </summary>
    public partial class SuaBenhNhanView : Window
    {
        public SuaBenhNhanView(BENHNHAN benhnhan)
        {
            InitializeComponent();
            SuaBenhNhanViewModel viewmodel = new SuaBenhNhanViewModel(this); 
            viewmodel.benhnhan = benhnhan;
            viewmodel.loadEditCurrent();
            this.DataContext = viewmodel;
        }
    }
}
