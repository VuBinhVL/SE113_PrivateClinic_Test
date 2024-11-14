using PrivateClinic.Model;
using PrivateClinic.ViewModel.HoSoBacSiVM;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrivateClinic.View.HoSoBacSi
{
    /// <summary>
    /// Interaction logic for PhanQuyenUS.xaml
    /// </summary>
    public partial class PhanQuyenUS : UserControl
    {
        public PhanQuyenUS()
        {
            InitializeComponent();
            PhanQuyenViewModel viewModel = new PhanQuyenViewModel();    
            this.DataContext = viewModel;
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PhanQuyenViewModel viewModel)
            {
                var selectedMedicine = (sender as Button)?.CommandParameter as PQDTO;

                if (selectedMedicine != null)
                {
                    viewModel.EditCommand.Execute(selectedMedicine);
                    ListViewPQ.Items.Refresh();
                }
            }
        }
    }
}
