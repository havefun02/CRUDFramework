﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDFramework.Cores
{
    internal class SortingCriterion
    {
        public string PropertyName { get; set; } = string.Empty; 
        public bool Descending { get; set; } 

    }
}