using PrivateClinic.Model;
using PrivateClinic.View.QuanLiKhoThuoc;
using PrivateClinic.ViewModel.OtherViewModels;
using System.Windows.Input;

namespace PrivateClinic.ViewModel.QuanLiKhoThuocVM
{
    public class ThongTinThuocViewModel
    {

        public ICommand ExitCommand { get; set; }

        // Hàm khởi tạo
        public ThongTinThuocViewModel()
        {
            ExitCommand = new RelayCommand<ThongTinThuocView>((p) => { return p == null ? false : true; }, (p) => _ExitCommand(p));
        }
        private void _ExitCommand(ThongTinThuocView p)
        {
            p.Close();
        }

    }
}
