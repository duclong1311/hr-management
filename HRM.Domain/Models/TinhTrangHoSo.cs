using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Entities;

namespace HRM.Domain.Models
{
    public class TinhTrangHoSo : IEntity
    {
        public bool SoYeuLyLich { get; set; }
        public bool CCCD_Copy { get; set; }
        public bool GiayKhaiSinh_Copy { get; set; }
        public bool GiayKhamSucKhoe { get; set; }
        public bool AnhThe { get; set; }
        public bool TinhTrangGiayTo { get; set; }
    }
}
