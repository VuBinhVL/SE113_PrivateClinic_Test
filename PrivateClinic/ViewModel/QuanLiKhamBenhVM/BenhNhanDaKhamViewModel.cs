using PrivateClinic.Model;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinic.ViewModel.QuanLiKhamBenhVM
{
    public class BenhNhanDaKhamViewModel : BaseViewModel
    {
        #region Các Command và Property
        private ObservableCollection<BenhNhanDTO> listBN;
        public ObservableCollection<BenhNhanDTO> ListBN
        {
            get => listBN; 
            set
            {
                listBN = value;
                OnPropertyChanged(nameof(ListBN));
            }
        }

        private ObservableCollection<BenhNhanDTO> filterlistBN;
        public ObservableCollection<BenhNhanDTO> FilterListBN
        {
            get => filterlistBN;
            set
            {
                filterlistBN = value;
                OnPropertyChanged(nameof(FilterListBN));

            }
        }

        private ObservableCollection<BENHNHAN> benhnhan;
        public ObservableCollection<BENHNHAN> BenhNhan
        {
            get => benhnhan;
            set
            {
                benhnhan = value;
                OnPropertyChanged(nameof(BenhNhan));
            }
        }

        private ObservableCollection<PHIEUKHAMBENH> listpkb;
        public ObservableCollection<PHIEUKHAMBENH> ListPKB
        {
            get => listpkb;
            set
            {
                listpkb = value;
                OnPropertyChanged(nameof(ListPKB));
            }
        }

        private ObservableCollection<LOAIBENH> listLoaiBenh;
        public ObservableCollection<LOAIBENH> ListLoaiBenh
        {
            get => listLoaiBenh;
            set
            {
                listLoaiBenh = value;
                OnPropertyChanged(nameof(ListLoaiBenh));
            }
        }

        private ObservableCollection<BenhNhanDTO> originalListBN;

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    ApplyFilter();
                }
            }
        }

        private int soluong;
        public int SoLuong
        {
            get => soluong;
            set
            {
                soluong = value;
                OnPropertyChanged(nameof(SoLuong));
            }
        }

        private DateTime? filterDate;
        public DateTime? FilterDate
        {
            get => filterDate;
            set
            {
                filterDate = value;
                OnPropertyChanged(nameof(FilterDate));
                ApplyFilter();
                SoLuongBNDaKhamHomNay();
            }
        }
        #endregion

        //Hàm khởi tạo
        public BenhNhanDaKhamViewModel()
        {
            LoadData();
            SoLuongBNDaKhamHomNay();
        }

        //Hàm load dữ liệu lên list view 
        void LoadData()
        {
            // Lấy tất cả phiếu khám bệnh
            ListPKB = new ObservableCollection<PHIEUKHAMBENH>(DataProvider.Ins.DB.PHIEUKHAMBENHs);
            // Lấy tất cả loại bệnh
            ListLoaiBenh = new ObservableCollection<LOAIBENH>(DataProvider.Ins.DB.LOAIBENHs);
            // Lấy tất cả bệnh nhân
            BenhNhan = new ObservableCollection<BENHNHAN>(DataProvider.Ins.DB.BENHNHANs);

            // Tạo từ điển để tra cứu nhanh
            var pkbDictionary = ListPKB.GroupBy(pkb => pkb.MaBN).ToDictionary(group => group.Key, group => group.ToList());
            var loaiBenhDictionary = ListLoaiBenh.ToDictionary(lb => lb.MaLoaiBenh, lb => lb.TenLoaiBenh);

            // Xử lý
            List<BenhNhanDTO> tempListBN = new List<BenhNhanDTO>();
            int stt = 1;

            foreach (var benhnhan in BenhNhan)
            {
                if (pkbDictionary.TryGetValue(benhnhan.MaBN, out var pkbList))
                {
                    foreach (var pkb in pkbList)
                    {
                        BenhNhanDTO benhNhanDTO = new BenhNhanDTO
                        {
                            HoTen = benhnhan.HoTen,
                            TrieuChung = pkb.TrieuChung,
                            NgayKham = pkb.NgayKham,
                            TenLoaiBenh = loaiBenhDictionary.TryGetValue(pkb.MaLoaiBenh, out var tenLoaiBenh) ? tenLoaiBenh : string.Empty
                        };
                        tempListBN.Add(benhNhanDTO);
                        
                    }
                }
            }
            // Sắp xếp theo NgayKham giảm dần
            var sortedListBN = tempListBN.OrderByDescending(bn => bn.NgayKham).ToList();
            originalListBN = new ObservableCollection<BenhNhanDTO>(sortedListBN);
            //Đặt STT
            foreach (var benhnhan in sortedListBN)
            {
                benhnhan.STT = stt;
                stt++;
            }
            ListBN = new ObservableCollection<BenhNhanDTO>(sortedListBN);
            SearchBN();
            
        }

        #region Chức năng search theo tên
        void SearchBN()
        {
            ApplyFilter();
        }

        private void FilterDSBN()
        {
            ApplyFilter();
        }

        #endregion

        //Chức năng filter theo ngày
        private void ApplyFilter()
        {
            IEnumerable<BenhNhanDTO> filteredList = originalListBN;

            // Lọc theo ngày nếu có giá trị
            if (filterDate.HasValue)
            {
                filteredList = filteredList.Where(bn => bn.NgayKham == filterDate.Value.Date);
            }

            // Lọc theo tìm kiếm tên nếu có giá trị
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filteredList = filteredList.Where(bn => bn.HoTen.ToLower().Contains(SearchText.ToLower()));
            }

            FilterListBN = new ObservableCollection<BenhNhanDTO>(filteredList);
        }

        //Hàm đếm số lượng bệnh nhân đã khám trong ngày
        private void SoLuongBNDaKhamHomNay()
        {
            int soluong = 0;
            foreach (var pkb in originalListBN)
            {
                if (filterDate.HasValue)
                {
                    if (pkb.NgayKham.Value.Date == filterDate.Value.Date)
                    {
                        soluong++;
                    }
                }
                else
                {
                    if (pkb.NgayKham.Value.Date == DateTime.UtcNow.Date)
                    {
                        soluong++;
                    }
                }
            }
            SoLuong = soluong;
        }

    }
}
