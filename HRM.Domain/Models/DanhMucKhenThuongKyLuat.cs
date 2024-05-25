using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Entities;

namespace HRM.Domain.Models
{
    public class DanhMucKhenThuongKyLuat : IEntity
    {
        public string? CapKhenThuongKyLuat { get; set; }
        public string? HinhThucKhenThuongKyLuat { get; set; }
    }
}
