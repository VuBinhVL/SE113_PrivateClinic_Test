using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateClinic.Model;
using PrivateClinic.View.QuanLiKhoThuoc;
using System.Windows.Input;
using System.Windows;
using PrivateClinic.ViewModel.OtherViewModels;
using PrivateClinic.View.QuanLiTiepDon;
using PrivateClinic.View.MessageBox;

namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    public class ThemCachDungViewModel:BaseViewModel
    {
        private ObservableCollection<THAMSO> _thamso;
        public ObservableCollection<THAMSO> thamso
        {
            get => _thamso;
            set { _thamso = value; OnPropertyChanged(nameof(thamso)); }

        }

        public ICommand SaveCommand { get; set; }
        public ThemCachDungViewModel()
        {
            SaveCommand = new RelayCommand<ThemCachDungUS>((p) => { return p == null ? false : true; }, (p) => _SaveCommand(p));
        }
        private void _SaveCommand(ThemCachDungUS p)
        {

            if (string.IsNullOrEmpty(p.dvttbx.Text))
            {
                OkMessageBox ok = new OkMessageBox("Thông báo", "Bạn chưa nhập đủ thông tin");
                ok.ShowDialog();
            }
            else
            {
                YesNoMessageBox h =new YesNoMessageBox("THÔNG BÁO", "Bạn muốn thêm cách dùng mới ?");
                h.ShowDialog();
                if (h.DialogResult == true)
                {


                    CACHDUNG cd = new CACHDUNG();
                    cd.TenCachDung = p.dvttbx.Text;
                    DataProvider.Ins.DB.CACHDUNGs.Add(cd);      
                    DataProvider.Ins.DB.SaveChanges();
                    OkMessageBox ok = new OkMessageBox("Thông báo", "Thêm thành công!");
                    ok.ShowDialog();
                }
            }
            
        }
    }
}
