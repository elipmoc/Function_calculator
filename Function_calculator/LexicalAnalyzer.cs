using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function_calculator
{

    //字句解析器
   public class LexicalAnalyzer
    {
        //字句解析実行
       static public TokenStream Lexicalanalysis(string str)
        {
            var tokenlist = new List<Token>();

            for (int index = 0; index < str.Length; index++)
            {

                //実数の解析
                if (IsNum(str[index]))
                {
                    int temp = index;
                    //少数点フラグ
                    bool tenFlag = false;
                    while (index < str.Length)
                    {
                        if (IsNum(str[index])) ;
                        else if (str[index] == '.') {
                            if (tenFlag == false)
                                tenFlag = true;
                            else return null;
                        }
                        else break;
                        index++;
                    }
                    index--;
                    tokenlist.Add(new Token(str.Substring(temp, index - temp + 1), TokenType.Double));
                }
                //演算子の解析
                else if (IsOperator(str[index]))
                {
                    tokenlist.Add(new Token(str[index].ToString(), TokenType.Operator));
                }
                //（の解析
                else if (str[index] == '(')
                {
                    tokenlist.Add(new Token("(", TokenType.LeftKakko));
                }
                //）の解析
                else if (str[index] == ')')
                {
                    tokenlist.Add(new Token(")", TokenType.RightKakko));
                }
                //{の解析
                else if (str[index] == '{')
                {
                    tokenlist.Add(new Token("{", TokenType.LeftTyuKakko));
                }
                //}の解析
                else if (str[index] == '}')
                {
                    tokenlist.Add(new Token("}", TokenType.RightTyuKakko));
                }
                //,の解析
                else if (str[index] == ',')
                {
                    tokenlist.Add(new Token(",",TokenType.Comma));
                }
                //識別子の解析
                else if (IsAlpha(str[index])){
                    int temp = index;
                    while (index < str.Length&& IsAlpha(str[index]))
                    {
                        index++;
                    }
                    index--;
                    tokenlist.Add(new Token(str.Substring(temp, index - temp + 1), TokenType.Identifier));
                }
                else return null;
            }

            return new TokenStream(tokenlist);
        }

        //数値判定
        static bool IsNum(char c)
            =>
                c == '0' ||
                c == '1' ||
                c == '2' ||
                c == '3' ||
                c == '4' ||
                c == '5' ||
                c == '6' ||
                c == '7' ||
                c == '8' ||
                c == '9';

        //演算子判定
        static bool IsOperator(char c)
            =>
                c == '+' ||
                c == '-' ||
                c == '*' ||
                c == '/' ||
                c == '=' ;
        static bool IsAlpha(char c)
            => ('a' <= c) && (c <='z');
    }
}
