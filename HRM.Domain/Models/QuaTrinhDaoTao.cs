using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Entities;

namespace HRM.Domain.Models
{
    public class QuaTrinhDaoTao : IEntity
    {
        public string TuNgayDenNgay { get; set; }
        public string NoiDaoTao { get; set; }
        public string HinhThucDaoTao { get; set; }
        public string VanBangChungChi { get; set; }
    }
}
