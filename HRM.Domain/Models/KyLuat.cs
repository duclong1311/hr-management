﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Entities;

namespace HRM.Domain.Models
{
    public class KyLuat : IEntity
    {
        public string CapKyLuat { get; set; }
        public string ThamQuyen { get; set; }
        public string NoiDung { get; set; }
        public string SoQuyetDinh { get; set; }
        public DateOnly ThoiGianBanHanh { get; set; }
    }
}
