﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDFramework.Interfaces
{
    internal interface IPaginationResult<T> where T : class
    {
        public int totalItems { get; set; }
        public List<T>? items { get; set; }
        public int limit { get; set; }
    }
}