using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function_calculator
{

    //字句解析器
    class LexicalAnalyzer
    {
        //字句解析実行
       static public TokenStream Lexicalanalysis(string str)
        {
            var tokenlist = new List<Token>();

            for (int index = 0; index < str.Length; index++)
            {

                if (IsNum(str[index]))
                {
                    int temp = index;
                    while (index < str.Length && IsNum(str[index]))
                    {
                        index++;
                    }
                    index--;
                    tokenlist.Add(new Token(str.Substring(temp, index - temp + 1), TokenType.Int));
                }
                else if (IsOperator(str[index]))
                {
                    tokenlist.Add(new Token(str[index].ToString(), TokenType.Operator));
                }
                else if (str[index] == '(')
                {
                    tokenlist.Add(new Token("(", TokenType.LeftKakko));
                }
                else if (str[index] == ')')
                {
                    tokenlist.Add(new Token(")", TokenType.RightKakko));
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
                c == '/';
    }
}
