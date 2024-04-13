using HRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Models
{
    public class HoSo : DomainObject
    {
        public string Anh { get; set; }
        public bool GioiTinh { get; set; }
        public DateTime SinhNgay { get; set; }
        public string NoiSinh { get; set; }
        public string NguyenQuan { get; set; }
        public string DanToc {  get; set; }
    }
}
