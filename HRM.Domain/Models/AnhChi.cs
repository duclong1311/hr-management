using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Entities;

namespace HRM.Domain.Models
{
    public class AnhChi : IEntity
    {
        public string HoTenAnhChi { get; set; }
        public DateTime NamSinh { get; set; }
        public string NgheNghiep { get; set; }
        public string CoQuanCongTac { get; set; }   
    }
}
