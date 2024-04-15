using HRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Models
{
    public class NhanSu : DomainObject
    {
        public string HoTen {  get; set; }
        public string EmailCongTy {  get; set; }
        public string STK { get; set; }
        public string MaSoBHXH {  get; set; }
        public string MaSoThue {  get; set; }
        public DateTime NgayVaoLam { get; set; }
        public double LuongCoBan {  get; set; }
    }
}
