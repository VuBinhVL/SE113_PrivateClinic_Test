using PrivateClinic.Model;
using PrivateClinic.ViewModel.QuanLiTiepDon;
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
using static MaterialDesignThemes.Wpf.Theme;
using Button = System.Windows.Controls.Button;

namespace PrivateClinic.View.QuanLiTiepDon
{
    /// <summary>
    /// Interaction logic for BenhNhanDangKhamView.xaml
    /// </summary>
    public partial class BenhNhanDangKhamView : UserControl
    {
        public BenhNhanDangKhamView()
        {
            InitializeComponent();
            BenhNhanDangKhamViewModel viewModel= new BenhNhanDangKhamViewModel();
            this.DataContext = viewModel;
        }
        public void SetData(BENHNHAN benhNhan)
        {
            MaBN.Text = benhNhan.MaBN.ToString();
            if (benhNhan.MaBN < 10)
            {
                MaBNformat.Text = "BN00" + benhNhan.MaBN.ToString();
            }
            else if (benhNhan.MaBN < 100)
            {
                MaBNformat.Text = "BN0" + benhNhan.MaBN.ToString();
            }
            else
                MaBNformat.Text= "BN" + benhNhan.MaBN.ToString(); 
            TenBN.Text = benhNhan.HoTen;
            NgsinhBN.Text = benhNhan.NamSinh.ToString("dd/MM/yyyy");
            GioiTinh.Text = benhNhan.GioiTinh;

            DateTime namSinh = benhNhan.NamSinh;
            int tuoi = DateTime.Now.Year - namSinh.Year;
            if (DateTime.Now < namSinh.AddYears(tuoi))
            {
                tuoi--;
            }
            Tuoi.Text = tuoi.ToString();

            NgKham.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is BenhNhanDangKhamViewModel viewModel)
            {
                var selected= (sender as System.Windows.Controls.Button)?.CommandParameter as ThuocDTO;

                if (selected != null)
                {
                    viewModel.EditThuocCommand.Execute(selected);

                }
            }
        }

    }
}
