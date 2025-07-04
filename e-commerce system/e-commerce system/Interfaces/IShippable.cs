using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_system.Interfaces
{
    public interface IShippable
    {
        double GetWeight();
        string GetName();
    }
}
