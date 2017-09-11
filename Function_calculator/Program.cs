﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function_calculator
{
    class Program
    {

        //拡張BNF記法
        //　[]は　0回か1回を表し
        //  {}を0回以上の繰り返し
        //　+を1回以上の繰り返しとする


        /*
        式
            : 項 , { "+" || "-" , 項 }
            ;

        項
            : 実数 || 関数 || ( "(" , 式 , ")" ) , { "*" || "/", 項 }
            ;

        関数
            :識別子,"(",引数,")"
            ;

        識別子
            :(a-z)+
            ;

        引数
            :[式 , {"," , 式}]
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
                /*for (int i = 0; i < tokenStream.Size; i++)
                    tokenStream[i].DebugPrint();*/
                var ast = CreateAST.CreateSikiAST(tokenStream);

                if (ast == null)
                    Console.WriteLine("syntax error!!");
                else
                    Console.WriteLine("result　" + ast.GetValue());
                Console.WriteLine();
            }
        }

    }

}
