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
using PrivateClinic.View.MessageBox;

namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    public class XoaCachDungViewModel:BaseViewModel
    {
        private ObservableCollection<CACHDUNG> _cachdung;
        public ObservableCollection<CACHDUNG> cachdung
        {
            get => _cachdung;
            set { _cachdung = value; OnPropertyChanged(nameof(cachdung)); }

        }
        private ObservableCollection<THAMSO> _thamso;
        public ObservableCollection<THAMSO> thamso
        {
            get => _thamso;
            set { _thamso = value; OnPropertyChanged(nameof(thamso)); }

        }
        private ObservableCollection<THUOC> _thuoc;
        public ObservableCollection<THUOC> thuoc
        {
            get => _thuoc;
            set { _thuoc = value; OnPropertyChanged(nameof(thuoc)); }

        }
        private List<string> _TenDVTs;
        public List<string> TenDVTs
        {
            get { return _TenDVTs; }
            set
            {
                _TenDVTs = value;
                OnPropertyChanged(nameof(TenDVTs));
            }
        }
        public ICommand LoadCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public XoaCachDungViewModel()
        {
            LoadCommand = new RelayCommand<XoaCachDungUS>((p) => { return p == null ? false : true; }, (p) => _LoadCommand(p));
            SaveCommand = new RelayCommand<XoaCachDungUS>((p) => { return p == null ? false : true; }, (p) => _SaveCommand(p));
        }
        private void _SaveCommand(XoaCachDungUS p)
        {
            if (p.dvtcbx.SelectedItem == null)
            {
                OkMessageBox ok = new OkMessageBox("Thông báo", "Bạn chưa chọn cách dùng để xóa");
                ok.ShowDialog();
            }
            else
            {
                YesNoMessageBox h = new YesNoMessageBox("THÔNG BÁO", "Bạn muốn xóa cách dùng này ?");
                h.ShowDialog();
                if (h.DialogResult == true)
                {
                    thuoc = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOCs);

                    cachdung = new ObservableCollection<CACHDUNG>(DataProvider.Ins.DB.CACHDUNGs);

                    foreach (var cd in cachdung)
                    {
                        if (cd.TenCachDung == p.dvtcbx.Text)
                        {
                            // Kiểm tra xem cách dùng này đã được sử dụng bởi thuốc chưa
                            if (thuoc.Any(t => t.MaCachDung == cd.MaCachDung))
                            {
                                OkMessageBox ok = new OkMessageBox("Thông báo", "Không thể xóa cách dùng này vì đã được sử dụng bởi một hoặc nhiều thuốc.");
                                ok.ShowDialog();
                                return; // Thoát khỏi hàm nếu không thể xóa
                            }
                            else
                            {
                                // Xóa cách dùng
                                DataProvider.Ins.DB.CACHDUNGs.Remove(cd);
                                p.dvtcbx.SelectedIndex = -1;
                                OkMessageBox ok = new OkMessageBox("Thông báo", "Xóa cách dùng thành công");
                                ok.ShowDialog();
                            }
                        }
                    }
                    

                }
            }
            
        }
        private void _LoadCommand(XoaCachDungUS p)
        {

            cachdung = new ObservableCollection<CACHDUNG>(DataProvider.Ins.DB.CACHDUNGs);
            TenDVTs = cachdung.Select(x => x.TenCachDung).ToList();
        }
    }
}
