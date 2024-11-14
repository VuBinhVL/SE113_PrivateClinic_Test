using PrivateClinic.Model;
using PrivateClinic.View.MessageBox;
using PrivateClinic.View.ThanhToan;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using static PrivateClinic.ViewModel.ThanhToan.ThanhToanViewModel;
using Color = System.Drawing.Color;
using FontStyle = System.Drawing.FontStyle;
using Pen = System.Drawing.Pen;

namespace PrivateClinic.ViewModel.ThanhToan
{
    public class HoaDonViewModel : BaseViewModel
    {
        #region Properties
        public static HoaDonViewModel Instance { get; } = new HoaDonViewModel();

        public HoaDonView hoaDonView { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand SetHoaDonView { get; set; }

        private HOADON _currentHoaDon;
        public HOADON CurrentHoaDon
        {
            get => _currentHoaDon;
            set
            {
                if (_currentHoaDon != value)
                {
                    _currentHoaDon = value;
                    OnPropertyChanged(nameof(CurrentHoaDon));


                    if (PhieuKhamBenh == null)
                    {
                        Debug.WriteLine("Không tìm thấy PHIEUKHAMBENH với MaPKB: " + _currentHoaDon.MaPKB);
                    }
                    else
                    {
                        Debug.WriteLine("Tìm thấy PHIEUKHAMBENH, Ngày Khám: " + PhieuKhamBenh.NgayKham);
                    }

                    UpdateMedicationList();
                }
            }
        }


        private PHIEUKHAMBENH _phieuKhamBenh;
        public PHIEUKHAMBENH PhieuKhamBenh
        {
            get => _phieuKhamBenh;
            set
            {
                _phieuKhamBenh = value;
                OnPropertyChanged(nameof(PhieuKhamBenh));
            }
        }


        private ObservableCollection<MedicationDetails> _listThuoc;
        public ObservableCollection<MedicationDetails> ListThuoc
        {
            get => _listThuoc;
            set
            {
                _listThuoc = value;
                OnPropertyChanged(nameof(ListThuoc));
            }
        }
        #endregion

        public HoaDonViewModel()
        {
            ListThuoc = new ObservableCollection<MedicationDetails>();
            SaveCommand = new RelayCommand<HoaDonView>((p) => true, (p) => _SaveCommand(p));
            CancelCommand = new RelayCommand<HoaDonView>((p) => true, p => _CancelCommand(p));
        }

        private void UpdateMedicationList()
        {
            ListThuoc.Clear();
            if (CurrentHoaDon != null && CurrentHoaDon.MaPKB != null)
            {
                PhieuKhamBenh = DataProvider.Ins.DB.PHIEUKHAMBENHs.FirstOrDefault(pkb => pkb.MaPKB == CurrentHoaDon.MaPKB);

                var listCT_PKB = DataProvider.Ins.DB.CT_PKB.Where(ct => ct.MaPKB == CurrentHoaDon.MaPKB).ToList();

                foreach (var ctPkb in listCT_PKB)
                {
                    var thuoc = DataProvider.Ins.DB.THUOCs.FirstOrDefault(t => t.MaThuoc == ctPkb.MaThuoc);
                    if (thuoc != null && ctPkb.SoLuong.HasValue)
                    {
                        ListThuoc.Add(new MedicationDetails
                        {
                            Thuoc = thuoc,
                            SoLuong = ctPkb.SoLuong.Value,
                            TienThuoc = (float)(ctPkb.SoLuong.Value * (thuoc.DonGiaBan ?? 0))
                        });
                    }
                }
            }
        }

        void _SaveCommand(HoaDonView parameter)
        {
            if (CurrentHoaDon == null)
            {
                OkMessageBox mbs = new OkMessageBox("Thông báo", "Hóa đơn không được chọn");
                mbs.ShowDialog();
                return;
            }
            YesNoMessageBox mb = new YesNoMessageBox("Thông báo", "Xác nhận thanh toán hóa đợn này?");
            mb.ShowDialog();
            if (mb.DialogResult == true)
            {
                HOADON invoiceToUpdate = DataProvider.Ins.DB.HOADONs.FirstOrDefault(hd => hd.SoHD == CurrentHoaDon.SoHD);
                if (invoiceToUpdate != null)
                {
                    invoiceToUpdate.TrangThai = ((int)PaymentStatus.Paid).ToString();
                    DataProvider.Ins.DB.SaveChanges();
                    OkMessageBox mbs = new OkMessageBox("Thông báo", "Thanh toán thành công");
                    mbs.ShowDialog();
                    ConfirmPrintInvoice(parameter);
                }
                else
                {
                    OkMessageBox mbs = new OkMessageBox("Thông báo", "Hóa đơn không tồn tại");
                    mbs.ShowDialog();
                    parameter.DialogResult = true;
                    parameter.Close();
                }
            }
        }



        void _CancelCommand(HoaDonView parameter)
        {
            parameter.Close();
        }

        private void ConfirmPrintInvoice(HoaDonView parameter)
        {
            YesNoMessageBox h = new YesNoMessageBox("THÔNG BÁO", "Bạn muốn in hóa đơn không ?");
            h.ShowDialog();
            if (h.DialogResult == true)
            {
                PrintInvoice();
            }

            parameter.DialogResult = true;
            parameter.Close();
        }


        private void PrintInvoice()
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(PrintPage);
            printDocument.Print();
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font titleFont = new Font("Segoe UI", 18, FontStyle.Bold);
            Font headerFont = new Font("Segoe UI", 14, FontStyle.Bold);
            Font bodyFont = new Font("Segoe UI", 12);
            float fontHeight = bodyFont.GetHeight();
            int startX = 10;
            int startY = 10;
            int offset = 40;

            graphics.DrawString("Hóa đơn thanh toán", titleFont, new SolidBrush(Color.Black), startX, startY);
            offset += titleFont.Height + 10;

            graphics.DrawString("Mã hóa đơn: " + CurrentHoaDon.SoHD, headerFont, new SolidBrush(Color.Black), startX, startY + offset);
            offset += headerFont.Height + 5;
            graphics.DrawString("Họ và tên: " + CurrentHoaDon.BENHNHAN.HoTen, headerFont, new SolidBrush(Color.Black), startX, startY + offset);
            offset += headerFont.Height + 5;

            string ngayKhamText = PhieuKhamBenh != null ? PhieuKhamBenh.NgayKham.ToString("dd/MM/yyyy") : "Không xác định";
            graphics.DrawString("Ngày khám: " + ngayKhamText, headerFont, new SolidBrush(Color.Black), startX, startY + offset);
            offset += headerFont.Height + 5;

            string namSinhText;
            if (CurrentHoaDon.BENHNHAN.NamSinh != null)
            {
                namSinhText = CurrentHoaDon.BENHNHAN.NamSinh.ToString("yyyy");
            }
            else
            {
                namSinhText = "Không xác định";
            }
            graphics.DrawString("Năm sinh: " + namSinhText, headerFont, new SolidBrush(Color.Black), startX, startY + offset);
            offset += headerFont.Height + 5;


            graphics.DrawString("Tổng tiền: " + CurrentHoaDon.TongTien.ToString("#,##0") + " VND", headerFont, new SolidBrush(Color.Black), startX, startY + offset);
            offset += headerFont.Height + 20;

            Pen pen = new Pen(Color.Black);
            graphics.DrawLine(pen, startX, startY + offset, startX + 600, startY + offset);
            offset += 10;

            graphics.DrawString("STT", headerFont, new SolidBrush(Color.Black), startX, startY + offset);
            graphics.DrawString("Tên thuốc", headerFont, new SolidBrush(Color.Black), startX + 40, startY + offset);
            graphics.DrawString("Số lượng", headerFont, new SolidBrush(Color.Black), startX + 250, startY + offset);
            graphics.DrawString("Đơn giá", headerFont, new SolidBrush(Color.Black), startX + 350, startY + offset);
            graphics.DrawString("Thành tiền", headerFont, new SolidBrush(Color.Black), startX + 450, startY + offset);
            offset += headerFont.Height + 10;

            graphics.DrawLine(pen, startX, startY + offset, startX + 600, startY + offset);
            offset += 10;

            for (int i = 0; i < ListThuoc.Count; i++)
            {
                var item = ListThuoc[i];
                graphics.DrawString((i + 1).ToString(), bodyFont, new SolidBrush(Color.Black), startX, startY + offset);
                graphics.DrawString(item.Thuoc.TenThuoc, bodyFont, new SolidBrush(Color.Black), startX + 40, startY + offset);
                graphics.DrawString(item.SoLuong.ToString(), bodyFont, new SolidBrush(Color.Black), startX + 250, startY + offset);
                graphics.DrawString(item.Thuoc.DonGiaBan?.ToString("#,##0") + " VND", bodyFont, new SolidBrush(Color.Black), startX + 350, startY + offset);
                graphics.DrawString(item.TienThuoc.ToString("#,##0") + " VND", bodyFont, new SolidBrush(Color.Black), startX + 450, startY + offset);
                offset += bodyFont.Height + 5;
            }

            graphics.DrawLine(pen, startX, startY + offset, startX + 600, startY + offset);
            offset += 20;

            float totalMedicationCost = ListThuoc.Sum(item => item.TienThuoc);
            graphics.DrawString("Tổng tiền thuốc: " + totalMedicationCost.ToString("#,##0") + " VND", headerFont, new SolidBrush(Color.Black), startX, startY + offset);
            offset += headerFont.Height + 5;

            graphics.DrawString("Tổng cộng: " + CurrentHoaDon.TongTien.ToString("#,##0") + " VND", headerFont, new SolidBrush(Color.Black), startX, startY + offset);
        }

        public class MedicationDetails : BaseViewModel
        {
            private THUOC _thuoc;
            public THUOC Thuoc
            {
                get => _thuoc;
                set
                {
                    _thuoc = value;
                    OnPropertyChanged(nameof(Thuoc));
                    OnPropertyChanged(nameof(TienThuoc));
                }
            }

            private int _soLuong;
            public int SoLuong
            {
                get => _soLuong;
                set
                {
                    _soLuong = value;
                    OnPropertyChanged(nameof(SoLuong));
                    OnPropertyChanged(nameof(TienThuoc));
                }
            }

            private float _tienThuoc;
            public float TienThuoc
            {
                get => _tienThuoc;
                set
                {
                    _tienThuoc = value;
                    OnPropertyChanged(nameof(TienThuoc));
                }
            }
        }
    }
}
