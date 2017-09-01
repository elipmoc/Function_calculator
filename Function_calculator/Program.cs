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

        static void Main(string[] args)
        {
            while (true)
            {
                var tokenStream=LexicalAnalyzer.Lexicalanalysis(Console.ReadLine());
                if(tokenStream==null)
                {
                    Console.WriteLine("syntax error!!");
                    continue;
                }
                //デバッグ用
                /*foreach (var item in tokenlist)
                {
                    item.DebugPrint();
                }*/
                var ast = CreateAST.CreateSikiAST(tokenStream);

                if (ast == null)
                    Console.WriteLine("syntax error!!");
                else
                    Console.WriteLine("result　" + ast.GetValue());
            }
        }

    }

}
