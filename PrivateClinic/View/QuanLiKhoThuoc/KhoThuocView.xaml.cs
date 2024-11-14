using PrivateClinic.Model;
using PrivateClinic.ViewModel.QuanLiKhoThuocVM;
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

namespace PrivateClinic.View.QuanLiKhoThuoc
{
    /// <summary>
    /// Interaction logic for KhoThuocView.xaml
    /// </summary>
    public partial class KhoThuocView : UserControl
    {
        public KhoThuocView()
        {
            InitializeComponent();
            KhoThuocViewModel viewModel = new KhoThuocViewModel();
            this.DataContext = viewModel;
        }
        private void EditMedicineButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is KhoThuocViewModel viewModel)
            {
                var selectedMedicine = (sender as Button)?.CommandParameter as ThuocDTO;

                if (selectedMedicine != null)
                {
                    viewModel.EditMedicineCommand.Execute(selectedMedicine);
                    MedicineListView.Items.Refresh();
                }
            }
        }
    }
}
