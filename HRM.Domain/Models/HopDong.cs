using HRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Models
{
    public class HopDong : IEntity
    {
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThu { get; set; }
        public string LoaiHopDong { get; set; }
        public bool CheckHopDong { get; set; }
        public bool CheckChuKy {  get; set; }
        public string ChiTietHopDong { get; set; }
        
    }
}
