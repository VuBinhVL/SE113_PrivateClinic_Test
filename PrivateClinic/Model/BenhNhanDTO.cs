using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinic.Model
{
    public class BenhNhanDTO
    {
        public int STT {  get; set; }
        public string HoTen { get; set; }
        public string TrieuChung { get; set; }
        public string TenLoaiBenh {  get; set; }

        public DateTime? NgayKham {  get; set; }
    }
}
