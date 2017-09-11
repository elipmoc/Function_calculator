using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function_calculator
{
    class VariableTable
    {
        Dictionary<string, double> table = new Dictionary<string, double>();
        public double? Find(string name)
        {
            if (table.ContainsKey(name) == false) return null;
            return table[name];
        }
        public void Register(string name,double value)
        {
            table[name] = value;
        }
    }
}
