﻿using HRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Models
{
    public class KhoanThu : DomainObject
    {
        public string TenKhoan {  get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien { get; set; }
        
    }
}
