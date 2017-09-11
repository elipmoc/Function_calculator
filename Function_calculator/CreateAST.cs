using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function_calculator
{
    public static class CreateAST
    {
        private static FunctionTable functionTable=new FunctionTable();

        public static BaseAST CreateSikiAST(TokenStream tokenst)
        {
            tokenst.SetCheckPoint();
            BaseAST baseAST = CreateKouAST(tokenst);
            if (baseAST == null) {
                tokenst.Rollback();
                return null;
            }
            while (tokenst.NowIndex < tokenst.Size)
            {
                string opstr = tokenst.Get().Str;
                if (opstr == "+" || opstr == "-")
                {
                    tokenst.Next();
                    BaseAST baseAST2 = CreateKouAST(tokenst);
                    if (baseAST2 == null)
                    {
                        tokenst.Rollback();
                        return null;
                    }
                    if (opstr == "+")
                        baseAST = new BinaryExprAST(BinaryExprAST.Op.Add, baseAST, baseAST2);
                    else
                        baseAST = new BinaryExprAST(BinaryExprAST.Op.Sub, baseAST, baseAST2);
                }
                else 
                    break;
            }
            return baseAST;
        }


        static BaseAST CreateKouAST(TokenStream tokenst)
        {
            BaseAST baseAST;
            if (tokenst.Get().TokenType == TokenType.Double)
            {
                baseAST = new DoubleAST(tokenst.Get().GetDouble());
                tokenst.Next();
            }
            else
            {
                baseAST = CreateFuncCallAST(tokenst);
                if (baseAST == null)
                {
                    if (tokenst.Get().TokenType == TokenType.LeftKakko)
                    {
                        tokenst.Next();
                        baseAST = CreateSikiAST(tokenst);
                        if (baseAST == null) return null;
                        if (tokenst.Get().TokenType != TokenType.RightKakko)
                            return null;
                        tokenst.Next();
                    }
                    else
                        return null;
                }
            }
            if (tokenst.NowIndex >= tokenst.Size) return baseAST;
            string opstr = tokenst.Get().Str;
            if (opstr == "*" || opstr == "/")
            {
                BaseAST baseAST2;
                tokenst.Next();
                baseAST2 = CreateKouAST(tokenst);
                if (baseAST2 == null) return null;
                if (opstr == "*")
                    return new BinaryExprAST(BinaryExprAST.Op.Mul, baseAST, baseAST2);
                else if (opstr == "/")
                    return new BinaryExprAST(BinaryExprAST.Op.Div, baseAST, baseAST2);
            }
            return baseAST;
        }

        static BaseAST CreateFuncCallAST(TokenStream tokenst)
        {
            tokenst.SetCheckPoint();
            if (tokenst.Get().TokenType == TokenType.Identifier)
            {
                var funcName = tokenst.Get().Str;
                tokenst.Next();
                if(tokenst.Get().TokenType != TokenType.LeftKakko)
                {
                    tokenst.Rollback();
                    return null;
                }
                tokenst.Next();
                var paramlist = CreateArgsAST(tokenst);
                if (paramlist == null)
                {
                    tokenst.Rollback();
                    return null;
                }
                var function = functionTable.FindFunction(funcName, paramlist.Count);
                if (function == null)
                {
                    tokenst.Rollback();
                    return null;
                }
                if (tokenst.Get().TokenType != TokenType.RightKakko)
                {
                    tokenst.Rollback();
                    return null;
                }
                tokenst.Next();
                return new FunctionAST(paramlist, function.func);
            }
            else return null;
        }

        static List<BaseAST> CreateArgsAST(TokenStream tokenst)
        {
            List<BaseAST> paramlist;
            tokenst.SetCheckPoint();
            var baseAST = CreateSikiAST(tokenst);
            if (baseAST == null)
            {
                return new List<BaseAST>();
            }
            paramlist = new List<BaseAST>();
            paramlist.Add(baseAST);
            while (tokenst.Get().TokenType == TokenType.Comma)
            {
                tokenst.Next();
                baseAST = CreateSikiAST(tokenst);
                if (baseAST == null)
                {
                    tokenst.Rollback();
                    return null;
                }
                paramlist.Add(baseAST);
            }
            return paramlist;
        }

    }
}
