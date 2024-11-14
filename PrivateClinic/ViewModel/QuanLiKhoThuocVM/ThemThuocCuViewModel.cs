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

    public class ThemThuocCuViewModel : BaseViewModel
    {
        private ObservableCollection<THUOC> _thuoc;
        public ObservableCollection<THUOC> thuoc
        {
            get => _thuoc;
            set { _thuoc = value; OnPropertyChanged(nameof(thuoc)); }

        }
        private List<string> _TenThuocs;
        public List<string> TenThuocs
        {
            get { return _TenThuocs; }
            set
            {
                _TenThuocs = value;
                OnPropertyChanged(nameof(TenThuocs));
            }
        }


        public ICommand LoadCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ThemThuocCuViewModel()
        {
            thuoc = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOCs);
            LoadCommand = new RelayCommand<ThemThuocCuView>((p) => true, (p) => _LoadCommand(p));
            SaveCommand = new RelayCommand<ThemThuocCuView>((p) => true, (p) => _SaveCommand(p));
        }
        private void _LoadCommand(ThemThuocCuView p)
        {
            thuoc = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOCs);
            TenThuocs = thuoc.Select(x => x.TenThuoc).ToList();

        }
        void _SaveCommand(ThemThuocCuView paramater)
        {
            if (string.IsNullOrEmpty(paramater.SL.Text) || paramater.ChonThuoccbx.SelectedItem == null)
            {
                OkMessageBox ok = new OkMessageBox("Thông báo", "Chưa nhập đủ thông tin");
                ok.ShowDialog();
            }
            else
            {
                if (!(int.TryParse(paramater.SL.Text, out int soLuong)) || soLuong <= 0)
                {
                    OkMessageBox ok = new OkMessageBox("Thông báo", "Số lượng không hợp lệ");
                    ok.ShowDialog();
                }
                else
                {



                    YesNoMessageBox h = new YesNoMessageBox("THÔNG BÁO", "Bạn muốn lưu thông tin cho thuốc ?");
                    h.ShowDialog();
                    if (h.DialogResult == true)
                    {
                        THUOC mathuoc = DataProvider.Ins.DB.THUOCs.FirstOrDefault(a => a.TenThuoc == paramater.ChonThuoccbx.SelectedItem.ToString());
                        THUOC thuoc = DataProvider.Ins.DB.THUOCs.FirstOrDefault(t => t.MaThuoc == mathuoc.MaThuoc);

                        if (thuoc != null)
                        {

                            thuoc.SoLuong += int.Parse(paramater.SL.Text);

                            DataProvider.Ins.DB.SaveChanges();
                            OkMessageBox ok = new OkMessageBox("Thông báo", "Lưu thành công");
                            ok.ShowDialog();
                            paramater.ChonThuoccbx.SelectedItem = -1;
                            paramater.SL.Clear();

                            KhoThuocView quanLiThuocView = new KhoThuocView();
                            quanLiThuocView.MedicineListView.ItemsSource = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOCs);
                            quanLiThuocView.MedicineListView.Items.Refresh();

                        }
                        else
                        {
                            OkMessageBox ok = new OkMessageBox("Lỗi", "Không tìm thấy thuốc");
                            ok.ShowDialog();
                        }
                    }
                }
            }
           
        }

    }
}
