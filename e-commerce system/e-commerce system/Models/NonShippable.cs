﻿using e_commerce_system.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_system.Models
{
    class NonShippable : IShippable
    {
        public string GetName() => "";

        public double GetWeight() => 0;
    }
}
