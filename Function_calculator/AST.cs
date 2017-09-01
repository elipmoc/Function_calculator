using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function_calculator
{
    public abstract class BaseAST
    {
        public abstract int GetValue();
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

        public override int GetValue()
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

    //IntAST
    class IntAST : BaseAST
    {
        int value;

        public override int GetValue()
        {
            return value;
        }

        public IntAST(int value)
        {
            this.value = value;
        }
    }
}
