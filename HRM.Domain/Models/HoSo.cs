using HRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Models
{
    public class HoSo : IEntity
    {
        public string Anh { get; set; }
        public bool GioiTinh { get; set; }
        public DateTime SinhNgay { get; set; }
        public string NoiSinh { get; set; }
        public string NguyenQuan { get; set; }
        public string DanToc { get; set; }
        public string TonGiao { get; set; }
        public string CCCD { get; set; }
        public DateTime CapNgay { get; set; }
        public string NoiCap { get; set; }
        public string TrinhDoVanHoa { get; set; }
        public string KetNapDoan { get; set; }
        public string NoiKetNapDoan { get; set; }
        public string KetNapDang { get; set; }
        public string NoiketNapDang {  get; set; }
        public string KhenThuong { get; set; }  
        public string SoThich { get; set; }
    }
}
