using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function_calculator
{
   public static class CreateAST
    {

       public static BaseAST CreateSikiAST(TokenStream tokenst)
        {
            BaseAST baseAST = CreateKouAST(tokenst);
            if (baseAST == null) return null;
            while (tokenst.nowIndex < tokenst.Size)
            {
                string opstr = tokenst[tokenst.nowIndex].Str;
                if (opstr == "+" || opstr == "-")
                {
                    tokenst.nowIndex++;
                    BaseAST baseAST2 = CreateKouAST(tokenst);
                    if (baseAST2 == null)
                        return null;
                    if (opstr == "+")
                        baseAST = new BinaryExprAST(BinaryExprAST.Op.Add, baseAST, baseAST2);
                    else
                        baseAST = new BinaryExprAST(BinaryExprAST.Op.Sub, baseAST, baseAST2);
                }
                else if(tokenst[tokenst.nowIndex].TokenType==TokenType.RightKakko)
                    break;
                else
                    return null;
            }
            return baseAST;
        } 


        static BaseAST CreateKouAST(TokenStream tokenst)
        {
            BaseAST baseAST;
            if (tokenst[tokenst.nowIndex].TokenType == TokenType.Int)
            {
                baseAST = new IntAST(tokenst[tokenst.nowIndex].GetInt());
                tokenst.nowIndex++;
            }
            else if (tokenst[tokenst.nowIndex].TokenType == TokenType.LeftKakko)
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
            if (tokenst.nowIndex >= tokenst.Size) return baseAST;
            string opstr = tokenst[tokenst.nowIndex].Str;
            if (opstr == "*" ||opstr == "/") {
                BaseAST baseAST2;
                tokenst.nowIndex++;
                baseAST2=CreateKouAST(tokenst);
                if (baseAST2 == null) return null;
                if (opstr == "*")
                    return new BinaryExprAST(BinaryExprAST.Op.Mul, baseAST, baseAST2);
                else if (opstr == "/")
                    return new BinaryExprAST(BinaryExprAST.Op.Div, baseAST, baseAST2);
            }
            return baseAST;
        }

    }
}
