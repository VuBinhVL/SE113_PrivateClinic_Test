using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PrivateClinic.Model;
using PrivateClinic.View.MessageBox;
using PrivateClinic.View.QuanLiKhoThuoc;
using PrivateClinic.ViewModel.OtherViewModels;
namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    public class ThemDonViTinhViewModel:BaseViewModel
    {
        private ObservableCollection<THAMSO> _thamso;
        public ObservableCollection<THAMSO> thamso
        {
            get => _thamso;
            set { _thamso = value; OnPropertyChanged(nameof(thamso)); }

        }
        
        public ICommand SaveCommand { get; set; }
        public ThemDonViTinhViewModel()
        {
            SaveCommand = new RelayCommand<ThemDonViTinhUS>((p) => { return p == null ? false : true; }, (p) => _SaveCommand(p));
        }
        private void _SaveCommand(ThemDonViTinhUS p)
        {
            if (string.IsNullOrEmpty(p.dvttbx.Text))
            {
                OkMessageBox ok = new OkMessageBox("Thông báo", "Chưa nhập đủ thông tin");
                ok.ShowDialog();
            }
            else
            {
                YesNoMessageBox h = new YesNoMessageBox("THÔNG BÁO", "Bạn muốn thêm đơn vị tính mới ?");
                h.ShowDialog();
                if (h.DialogResult == true)
                {

                    DVT dVT = new DVT();
                    dVT.TenDVT = p.dvttbx.Text;
                    DataProvider.Ins.DB.DVTs.Add(dVT);
                    DataProvider.Ins.DB.SaveChanges();
                    OkMessageBox ok = new OkMessageBox("Thông báo", "Thêm thành công!");
                    ok.ShowDialog();

                }
            }

           
        }
    }
}
