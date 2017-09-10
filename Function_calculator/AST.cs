using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function_calculator
{
    public abstract class BaseAST
    {
        public abstract double GetValue();
    }
    

    //二項演算子
    class BinaryExprAST : BaseAST
    {
        BaseAST left;
        BaseAST right;
        Op op;
        public enum Op
        {
            Add,Sub,Mul,Div
        };

        public override double GetValue()
        {
            switch (op)
            {
                case Op.Mul:
                    return left.GetValue() * right.GetValue();
                case Op.Div:
                    return left.GetValue() / right.GetValue();
                case Op.Add:
                    return left.GetValue() + right.GetValue();
                case Op.Sub:
                    return left.GetValue() - right.GetValue();
            }
            return 114514;
            
        }

        public BinaryExprAST(Op op,BaseAST left,BaseAST right)
        {
            this.op = op;
            this.left = left;
            this.right = right;
        }
    }

    //関数
    class FunctionAST : BaseAST
    {
        List<BaseAST> paramList;
        Func<List<BaseAST>, double> func;

        public override double GetValue()
        {
            return func(paramList);
        }

        public FunctionAST(List<BaseAST>paramList,Func<List<BaseAST>,double> func)
        {
            this.paramList = paramList;
            this.func = func;
        }
    }

    //DoubleAST
    class DoubleAST : BaseAST
    {
        double value;

        public override double GetValue()
        {
            return value;
        }

        public DoubleAST(double value)
        {
            this.value = value;
        }
    }
}
