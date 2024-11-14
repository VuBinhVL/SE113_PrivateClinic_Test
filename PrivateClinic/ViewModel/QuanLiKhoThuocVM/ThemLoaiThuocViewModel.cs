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
    public class ThemLoaiThuocViewModel : BaseViewModel
    {
        private ObservableCollection<THAMSO> _thamso;
        public ObservableCollection<THAMSO> thamso
        {
            get => _thamso;
            set { _thamso = value; OnPropertyChanged(nameof(thamso)); }

        }

        public ICommand SaveCommand { get; set; }
        public ThemLoaiThuocViewModel()
        {
            SaveCommand = new RelayCommand<ThemLoaiThuocUS>((p) => { return p == null ? false : true; }, (p) => _SaveCommand(p));
        }
        private void _SaveCommand(ThemLoaiThuocUS p)
        {
            if (string.IsNullOrEmpty(p.dvttbx.Text))
            {
                OkMessageBox ok = new OkMessageBox("Thông báo", "Chưa nhập đủ thông tin");
                ok.ShowDialog();
            }
            else
            {
                YesNoMessageBox h = new YesNoMessageBox("THÔNG BÁO", "Bạn muốn thêm loại thuốc mới ?");
                h.ShowDialog();
                if (h.DialogResult == true)
                {

                    LOAITHUOC dVT = new LOAITHUOC();
                    dVT.TenLoaiThuoc = p.dvttbx.Text;
                    DataProvider.Ins.DB.LOAITHUOCs.Add(dVT);
                    thamso = new ObservableCollection<THAMSO>(DataProvider.Ins.DB.THAMSOes);
                    DataProvider.Ins.DB.SaveChanges();
                    OkMessageBox ok = new OkMessageBox("Thông báo", "Thêm thành công!");
                    ok.ShowDialog();

                }
            }
           
        }
    }
}
