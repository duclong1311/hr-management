using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Entities;

namespace HRM.Domain.Models
{
    public class QuanHeGiaDinh : IEntity
    {
        public string HoTenCha { get; set; }
        public DateTime NgaySinhCha {  get; set; }
        public string NgheNghiep { get; set; }
        public string CoQuanCongTacCha { get; set; }   
        public string ChoOCha { get; set; }
        public string HoTenMe { get; set; }
        public DateTime NgaySinhMe { get; set; }
        public string NgheNghiepMe { get; set; }
        public string CoQuanCongTacMe { get; set; }
        public string ChoOMe { get; set; }
    }
}
