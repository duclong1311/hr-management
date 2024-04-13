﻿using HRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Models
{
    public class User : DomainObject
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FingerprintCode { get; set; }
        public virtual Role Role { get; set; }
    }
}
