using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function_calculator
{
    class Program
    {
        /*
        式
            : 項 , ( "+" || "-" , 項 )*
            ;

        項
            : 整数 || ( "(" , 式 , ")" ) , ( "*" || "/", 項 )*
            ;

        整数
        　　: 数字+
            ;
             
        数字
            :"0"||"1"||"2"||"3"||"4"||"5"||"6"||"7"||"8"||"9"
            ;
               
        */

        static List<Token> tokenlist=new List<Token>();

        static void Main(string[] args)
        {
            while (true)
            {
                Lexicalanalysis(Console.ReadLine());
                //デバッグ用
               /* foreach (var item in tokenlist)
                {
                    item.DebugPrint();
                }*/
                Console.WriteLine("result" + CreateAST.CreateSikiAST(new TokenStream(tokenlist)).GetValue());
                tokenlist.Clear();
            }
        }


        static void Lexicalanalysis(string str)
        {

            for (int index = 0; index < str.Length; index++)
            {
                
                if (IsNum(str[index]))
                {
                    int temp = index;
                    while (index<str.Length && IsNum(str[index]))
                    {
                        index++;
                    }
                    index--;
                    tokenlist.Add(new Token(str.Substring(temp,index-temp+1),TokenType.Int));
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
            }
        }

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

        static bool IsOperator(char c)
            =>
                c == '+' ||
                c == '-' ||
                c == '*' ||
                c == '/';
    }

}
