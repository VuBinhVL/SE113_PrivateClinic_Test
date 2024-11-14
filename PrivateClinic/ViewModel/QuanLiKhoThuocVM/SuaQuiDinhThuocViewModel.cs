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
    public class SuaQuiDinhThuocViewModel: BaseViewModel
    {
        //Lấy danh sách thuốc để cập nhật giá bán khi thay đổi tỉ lệ bán
        private ObservableCollection<THUOC> listthuoc;
        public ObservableCollection<THUOC> Listthuoc
        {
            get => listthuoc;
            set { listthuoc = value; OnPropertyChanged(nameof(Listthuoc)); }

        }
        public ICommand CancelCommand { get; set; }
        private ObservableCollection<THAMSO> _thamso;
        public ObservableCollection<THAMSO> thamso
        {
            get => _thamso;
            set { _thamso = value; OnPropertyChanged(nameof(thamso)); }

        }
        private ObservableCollection<CACHDUNG> _cachdung;
        public ObservableCollection<CACHDUNG> cachdung
        {
            get => _cachdung;
            set { _cachdung = value; OnPropertyChanged(nameof(cachdung)); }

        }
        private ObservableCollection<LOAIBENH> _Loaibenh;
        public ObservableCollection<LOAIBENH> Loaibenh
        {
            get => _Loaibenh;
            set { _Loaibenh = value; OnPropertyChanged(nameof(Loaibenh)); }

        }
        private ObservableCollection<LOAITHUOC> _Loaithuoc;
        public ObservableCollection<LOAITHUOC> Loaithuoc
        {
            get => _Loaithuoc;
            set { _Loaithuoc = value; OnPropertyChanged(nameof(Loaithuoc)); }

        }
        private ObservableCollection<DVT> _Dvt;
        public ObservableCollection<DVT> Dvt
        {
            get => _Dvt;
            set { _Dvt = value; OnPropertyChanged(nameof(Dvt)); }

        }
        private float _CachDung;
        public float CachDung
        {
            get { return _CachDung; }
            set
            {
                _CachDung = value;
                OnPropertyChanged(nameof(CachDung));
            }
        }
        private double _Tilegia;
        public double Tilegia
        {
            get { return _Tilegia; }
            set
            {
                _Tilegia = value;
                OnPropertyChanged(nameof(Tilegia));
            }
        }
        private float _dvt;
        public float dvt
        {
            get { return _dvt; }
            set
            {
                _dvt = value;
                OnPropertyChanged(nameof(dvt));
            }
        }
        private double _tienkham;
        public double tienkham
        {
            get { return _tienkham; }
            set
            {
                _tienkham = value;
                OnPropertyChanged(nameof(tienkham));
            }
        }
        private float _loaibenh;
        public float loaibenh
        {
            get { return _loaibenh; }
            set
            {
                _loaibenh = value;
                OnPropertyChanged(nameof(loaibenh));
            }
        }
        private float _loaithuoc;
        public float loaithuoc
        {
            get { return _loaithuoc; }
            set
            {
                _loaithuoc = value;
                OnPropertyChanged(nameof(loaithuoc));
            }
        }
        public ICommand SaveCommand { get; set; }
        public ICommand EditDvtCommand { get; set; }    
        public ICommand EditCachDungCommand { get; set; }    
        public ICommand EditSLBenhCommand { get; set; }    
        public ICommand EditSLThuocCommand { get; set; }    
        public SuaQuiDinhThuocViewModel()
        {
            Loaddata();
            CancelCommand = new RelayCommand<ThayDoiQuiDinhThuocView>((p) => { return p == null ? false : true; }, (p) => _ExitCommand(p));
            SaveCommand = new RelayCommand<ThayDoiQuiDinhThuocView>((p) => true, (p) => _SaveCommand(p));
            EditDvtCommand = new RelayCommand<ThayDoiQuiDinhThuocView>((p) => true, (p) => _EditDvtCommand(p));
            EditCachDungCommand = new RelayCommand<ThayDoiQuiDinhThuocView>((p) => true, (p) => _EditCachDungCommand(p));
            EditSLBenhCommand = new RelayCommand<ThayDoiQuiDinhThuocView>((p) => true, (p) => _EditSLBenhCommand(p));
            EditSLThuocCommand = new RelayCommand<ThayDoiQuiDinhThuocView>((p) => true, (p) => _EditSLThuocCommand(p));
        }
        private void _EditDvtCommand(ThayDoiQuiDinhThuocView p)
        {
            SuaDonViTinhView a =  new SuaDonViTinhView();
            a.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            a.ShowDialog();
            Loaddata();
        }
        private void _EditCachDungCommand(ThayDoiQuiDinhThuocView p)
        {
            SuaSoCachDungView a = new SuaSoCachDungView();
            a.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            a.ShowDialog();
            Loaddata();
        }
        private void _EditSLBenhCommand(ThayDoiQuiDinhThuocView p)
        {
            SuaLoaiBenhView a = new SuaLoaiBenhView();
            a.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            a.ShowDialog();
            Loaddata();
        }
        private void _EditSLThuocCommand(ThayDoiQuiDinhThuocView p)
        {
            SuaLoaiThuocView a = new SuaLoaiThuocView();
            a.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            a.ShowDialog();
            Loaddata();
        }
        void Loaddata()
        {
            thamso = new ObservableCollection<THAMSO>(DataProvider.Ins.DB.THAMSOes);
            //Lấy tỉ lệ giá bán
            THAMSO tlb = DataProvider.Ins.DB.THAMSOes.SingleOrDefault(h => h.MaThamSo == 3);
            //Lấy tiền khám
            THAMSO tk = DataProvider.Ins.DB.THAMSOes.SingleOrDefault(h => h.MaThamSo == 2);

            cachdung = new ObservableCollection<CACHDUNG>(DataProvider.Ins.DB.CACHDUNGs);
            Loaibenh = new ObservableCollection<LOAIBENH>(DataProvider.Ins.DB.LOAIBENHs);
            Loaithuoc = new ObservableCollection<LOAITHUOC>(DataProvider.Ins.DB.LOAITHUOCs);
            Dvt = new ObservableCollection<DVT>(DataProvider.Ins.DB.DVTs);
            CachDung = cachdung.Count();
            Tilegia = tlb.GiaTri;
            tienkham = tk.GiaTri;
            //tienkham = thamso.Where(x => x.MaThamSo == 2).Select(x => x.GiaTri.ToString()).ToList();
            //tienkham = thamso.Where(x => x.MaThamSo == 2).Select(x => x.GiaTri).ToList();
            loaibenh = Loaibenh.Count();
            loaithuoc = Loaithuoc.Count();
            dvt = Dvt.Count();

        }

        private void _ExitCommand(ThayDoiQuiDinhThuocView p)
        {
            p.Close();
        }
        private void _SaveCommand(ThayDoiQuiDinhThuocView p)
        {
            thamso = new ObservableCollection<THAMSO>(DataProvider.Ins.DB.THAMSOes);
            Listthuoc = new ObservableCollection<THUOC>(DataProvider.Ins.DB.THUOCs);
            if (!double.TryParse(p.Tilegia.Text, out double tile ) || tile < 0)
            {
                OkMessageBox mb = new OkMessageBox("Thông báo", "Tỉ lệ giá không hợp lệ!");
                mb.ShowDialog();
            }
            else if (!double.TryParse(p.TienKham.Text, out double tienkham) || tienkham < 0)
            {
                OkMessageBox mb = new OkMessageBox("Thông báo", "Tiền khám không hợp lệ!");
                mb.ShowDialog();
            }
            else
            {
                YesNoMessageBox mb = new YesNoMessageBox("Thông báo", "Bạn có muốn cập thông tin các quy định không");
                mb.ShowDialog();
                if (mb.DialogResult == true)
                {
                    // Tìm phần tử có MaThamSo == 2 và cập nhật tiền khám
                    var thamSo2 = thamso.FirstOrDefault(t => t.MaThamSo == 2);
                    if (thamSo2 != null)
                    {
                        thamSo2.GiaTri = tienkham;
                    }

                    // Tìm phần tử có MaThamSo == 3 và cập nhật tỉ lệ bán
                    var thamSo3 = thamso.FirstOrDefault(t => t.MaThamSo == 3);
                    if (thamSo3 != null)
                    {
                        thamSo3.GiaTri = Tilegia;
                    }
                    foreach (var thuoc in Listthuoc)
                    {
                        thuoc.DonGiaBan = thuoc.DonGiaNhap * Tilegia;
                    }
                    DataProvider.Ins.DB.SaveChanges();
                    OkMessageBox mb2 = new OkMessageBox("Thông báo", "Cập nhật quy định mới thành công!");
                    mb2.ShowDialog();
                    p.Close();
                }

            }
                
            }
        }
    }

