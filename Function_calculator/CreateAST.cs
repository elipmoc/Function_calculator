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

        private static VariableTable variableTable = new VariableTable();

        //文の解析
        public static BaseAST CreateStatementAST(TokenStream tokenst)
        {
            BaseAST baseAST = CreateSikiAST(tokenst);
            if (baseAST == null)
            {
                baseAST = CreateVariableDeclarationAST(tokenst);
            }
            if (tokenst.NowIndex != tokenst.Size)
                return null;
            return baseAST;
        }

        //変数宣言の解析
        public static BaseAST CreateVariableDeclarationAST(TokenStream tokenst)
        {
            tokenst.SetCheckPoint();
            string variableName;
            if (tokenst.Get().TokenType == TokenType.Identifier)
            {
                variableName = tokenst.Get().Str;
                tokenst.Next();
            }
            else
            {
                tokenst.Rollback();
                return null;
            }
            if (tokenst.Get().Str == "="){
                tokenst.Next();
                var exprAST = CreateSikiAST(tokenst);
                if (exprAST == null)
                {
                    tokenst.Rollback();
                    return null;
                }
                if (variableTable.Find(variableName) != null)
                {
                    tokenst.Rollback();
                    Console.WriteLine("変数が再宣言されました");
                    return null;
                }
                return new VariableDeclarationAST(variableTable,variableName, exprAST);

            }
            else
            {
                tokenst.Rollback();
                return null;
            }
        }

        //式の解析
        public static ExprAST CreateSikiAST(TokenStream tokenst)
        {
            tokenst.SetCheckPoint();
            ExprAST exprAST = CreateKouAST(tokenst);
            if (exprAST == null) {
                tokenst.Rollback();
                return null;
            }
            while (tokenst.NowIndex < tokenst.Size)
            {
                string opstr = tokenst.Get().Str;
                if (opstr == "+" || opstr == "-")
                {
                    tokenst.Next();
                    ExprAST baseAST2 = CreateKouAST(tokenst);
                    if (baseAST2 == null)
                    {
                        tokenst.Rollback();
                        return null;
                    }
                    if (opstr == "+")
                        exprAST = new BinaryExprAST(BinaryExprAST.Op.Add, exprAST, baseAST2);
                    else
                        exprAST = new BinaryExprAST(BinaryExprAST.Op.Sub, exprAST, baseAST2);
                }
                else 
                    break;
            }
            return exprAST;
        }

        //項の解析
        static ExprAST CreateKouAST(TokenStream tokenst)
        {
            ExprAST exprAST;
            bool minusFlag = false;
            //実数
            if (tokenst.Get().Str == "-")
            {
                minusFlag = true;
                tokenst.Next();
            }
            if (tokenst.Get().TokenType == TokenType.Double)
            {
                exprAST = new DoubleAST(tokenst.Get().GetDouble());
                tokenst.Next();
            }
            //関数
            else
            {
                exprAST = CreateFuncCallAST(tokenst);
                
                if (exprAST == null)
                {
                    //変数
                    if (tokenst.Get().TokenType == TokenType.Identifier && variableTable.Find(tokenst.Get().Str) != null)
                    {
                        exprAST = new DoubleAST(variableTable.Find(tokenst.Get().Str).Value);
                        tokenst.Next();
                    }

                    //（式）
                    else if (tokenst.Get().TokenType == TokenType.LeftKakko)
                    {
                        tokenst.Next();
                        exprAST = CreateSikiAST(tokenst);
                        if (exprAST == null) return null;
                        if (tokenst.Get().TokenType != TokenType.RightKakko)
                            return null;
                        tokenst.Next();
                    }
                    else
                        return null;

                }

            }
            if (tokenst.NowIndex >= tokenst.Size) return minusFlag ?exprAST = new MinusAST(exprAST) : exprAST;
            string opstr = tokenst.Get().Str;
            if (opstr == "*" || opstr == "/")
            {
                ExprAST baseAST2;
                tokenst.Next();
                baseAST2 = CreateKouAST(tokenst);
                if (baseAST2 == null) return null;
                if (opstr == "*")
                    exprAST= new BinaryExprAST(BinaryExprAST.Op.Mul, exprAST, baseAST2);
                else if (opstr == "/")
                    exprAST =new BinaryExprAST(BinaryExprAST.Op.Div, exprAST, baseAST2);
                return minusFlag ? exprAST = new MinusAST(exprAST) : exprAST;
            }
            return minusFlag ? exprAST = new MinusAST(exprAST) : exprAST;
        }

        //関数の解析
        static ExprAST CreateFuncCallAST(TokenStream tokenst)
        {
            tokenst.SetCheckPoint();
            if (tokenst.Get().TokenType == TokenType.Identifier)
            {
                var funcName = tokenst.Get().Str;
                tokenst.Next();
                if(tokenst.NowIndex>=tokenst.Size ||tokenst.Get().TokenType != TokenType.LeftKakko)
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

        //関数の引数の解析
        static List<ExprAST> CreateArgsAST(TokenStream tokenst)
        {
            List<ExprAST> paramlist;
            tokenst.SetCheckPoint();
            var baseAST = CreateSikiAST(tokenst);
            if (baseAST == null)
            {
                return new List<ExprAST>();
            }
            paramlist = new List<ExprAST>();
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
