﻿using HRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Models
{
    public class Role : IEntity
    {
        public string Name { get; set; }
        public string Permission { get; set; }
    }
}
