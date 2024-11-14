using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinic.Model
{
    public class Const : BaseViewModel
    {
        public static string TenDangNhap { get; set; }
        public static bool Admin { get; set; }
        public static NGUOIDUNG ND { get; set; }
        public static PHANQUYEN PQ { get; set; }
        public static ObservableCollection<ThuocDTO> ListThuoc { get; set; }
        public static ObservableCollection<ThuocDTO> ListThuocTemp { get; set; }


        public static string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
    }
}
