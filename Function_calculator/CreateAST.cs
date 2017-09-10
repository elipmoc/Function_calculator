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
            int backUp = tokenst.nowIndex;
            BaseAST baseAST = CreateKouAST(tokenst);
            if (baseAST == null) {
                tokenst.nowIndex = backUp;
                return null;
            }
            while (tokenst.nowIndex < tokenst.Size)
            {
                string opstr = tokenst[tokenst.nowIndex].Str;
                if (opstr == "+" || opstr == "-")
                {
                    tokenst.nowIndex++;
                    BaseAST baseAST2 = CreateKouAST(tokenst);
                    if (baseAST2 == null)
                    {
                        tokenst.nowIndex = backUp;
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
            if (tokenst[tokenst.nowIndex].TokenType == TokenType.Double)
            {
                baseAST = new DoubleAST(tokenst[tokenst.nowIndex].GetDouble());
                tokenst.nowIndex++;
            }
            else
            {
                baseAST = CreateFuncCallAST(tokenst);
                if (baseAST == null)
                {
                    if (tokenst[tokenst.nowIndex].TokenType == TokenType.LeftKakko)
                    {
                        tokenst.nowIndex++;
                        baseAST = CreateSikiAST(tokenst);
                        if (baseAST == null) return null;
                        if (tokenst[tokenst.nowIndex].TokenType != TokenType.RightKakko)
                            return null;
                        tokenst.nowIndex++;
                    }
                    else
                        return null;
                }
            }
            if (tokenst.nowIndex >= tokenst.Size) return baseAST;
            string opstr = tokenst[tokenst.nowIndex].Str;
            if (opstr == "*" || opstr == "/")
            {
                BaseAST baseAST2;
                tokenst.nowIndex++;
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
            int backUp = tokenst.nowIndex;
            if (tokenst[tokenst.nowIndex].TokenType == TokenType.Identifier)
            {
                var funcName = tokenst[tokenst.nowIndex].Str;
                tokenst.nowIndex++;
                if(tokenst[tokenst.nowIndex].TokenType != TokenType.LeftKakko)
                {
                    tokenst.nowIndex = backUp;
                    return null;
                }
                tokenst.nowIndex++;
                var paramlist = CreateArgsAST(tokenst);
                if (paramlist == null)
                {
                    tokenst.nowIndex = backUp;
                    return null;
                }
                var function = functionTable.FindFunction(funcName, paramlist.Count);
                if (function == null)
                {
                    tokenst.nowIndex = backUp;
                    return null;
                }
                if (tokenst[tokenst.nowIndex].TokenType != TokenType.RightKakko)
                {
                    tokenst.nowIndex = backUp;
                    return null;
                }
                tokenst.nowIndex++;
                return new FunctionAST(paramlist, function.func);
            }
            else return null;
        }

        static List<BaseAST> CreateArgsAST(TokenStream tokenst)
        {
            List<BaseAST> paramlist;
            int backUp = tokenst.nowIndex;
            var baseAST = CreateSikiAST(tokenst);
            if (baseAST == null)
            {
                return new List<BaseAST>();
            }
            paramlist = new List<BaseAST>();
            paramlist.Add(baseAST);
            while (tokenst[tokenst.nowIndex].TokenType == TokenType.Comma)
            {
                tokenst.nowIndex++;
                baseAST = CreateSikiAST(tokenst);
                if (baseAST == null)
                {
                    tokenst.nowIndex = backUp;
                    return null;
                }
                paramlist.Add(baseAST);
            }
            return paramlist;
        }

    }
}
