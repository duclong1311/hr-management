﻿using HRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Models
{
    public class HopDong : IEntity
    {
        public NhanSu MaNhanVien { get; set;}
        public string SoHopDong { get; set;}
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string LoaiHopDong { get; set; }
        public string ChiTietHopDong { get; set; }
    }
}
