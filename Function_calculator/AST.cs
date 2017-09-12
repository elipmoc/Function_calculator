using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function_calculator
{

    public interface BaseAST
    {
        void Do();
    }

    public interface ExprAST:BaseAST
    {
        double GetValue();
    }
    

    //二項演算子
    class BinaryExprAST : ExprAST
    {
        readonly ExprAST left;
        readonly ExprAST right;
        Op op;
        public enum Op
        {
            Add,Sub,Mul,Div
        };

        public void Do()
        {
            GetValue();
        }

        public double GetValue()
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

        public BinaryExprAST(Op op,ExprAST left,ExprAST right)
        {
            this.op = op;
            this.left = left;
            this.right = right;
        }
    }

    //関数
    class FunctionAST : ExprAST
    {
        readonly List<ExprAST> paramList;
        readonly Func<List<ExprAST>, double> func;

        public void Do()
        {
            GetValue();
        }

        public double GetValue()
        {
            return func(paramList);
        }

        public FunctionAST(List<ExprAST>paramList,Func<List<ExprAST>,double> func)
        {
            this.paramList = paramList;
            this.func = func;
        }
    }

    //実数
    class DoubleAST : ExprAST
    {
        readonly double value;

        public void Do()
        {
            GetValue();
        }

        public double GetValue()
        {
            return value;
        }

        public DoubleAST(double value)
        {
            this.value = value;
        }
    }

    class MinusAST : ExprAST
    {
        private readonly ExprAST exprAST;
        public MinusAST(ExprAST exprAST)
        {
            this.exprAST = exprAST;
        }

        public void Do()
        {
            exprAST.GetValue();
        }

        public double GetValue()
        {
            return -exprAST.GetValue();
        }
    }

    //変数宣言
    class VariableDeclarationAST:BaseAST
    {
        private readonly ExprAST exprAST;
        private readonly VariableTable vtable;
        private readonly string name;
        public VariableDeclarationAST(VariableTable vtable,string name,ExprAST baseAST)
        {
            this.name=name;
            this.exprAST = baseAST;
            this.vtable = vtable;
        }
        public void Do()
        {
            vtable.Register(name, exprAST.GetValue());
        }
    }


}
