using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function_calculator
{
    class Function
    {
        public readonly int paramNum;
        public readonly Func<List<BaseAST>, double> func;
        public Function(int paramNum, Func<List<BaseAST>, double> func)
        {
            this.paramNum = paramNum;
            this.func = func;
        }
    }

    class FunctionTable
    {
        Dictionary<string, Function> table=new Dictionary<string, Function>();

        public FunctionTable()
        {
            table.Add(
                "abs",
                new Function(1, (List<BaseAST> baseASTs) => Math.Abs(baseASTs[0].GetValue()))
                );
            table.Add(
                "pi",
                new Function(0, (List<BaseAST> baseASTs) => Math.PI));
            table.Add(
                "rad",
                new Function(1, (List<BaseAST> baseASTs) => baseASTs[0].GetValue() / 180 * Math.PI));
            table.Add(
                "max",
                new Function(2, (List<BaseAST> baseASTs) =>
                    Math.Max( baseASTs[0].GetValue(),baseASTs[1].GetValue())
                ));
        }

       public Function FindFunction(string name,int paramNum)
        {
            if(table.ContainsKey(name)==false)return null;
            if (table[name].paramNum == paramNum)
                return table[name];
            return null;
        }
    }
}
