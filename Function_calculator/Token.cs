using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function_calculator
{
    enum TokenType
    {
        Int,
        LeftKakko,
        RightKakko,
        Operator
    }

    class Token
    {
        readonly string str;

        readonly TokenType tokenType;

        public Token(string str,TokenType tokenType)
        {
            this.str = str;
            this.tokenType = tokenType;
        }

        public string Str { get { return str; } }

        public TokenType TokenType {get { return tokenType; } }

        public int GetInt()
        { 
            if (tokenType != TokenType.Int)
                throw new Exception("ばーか");
            return int.Parse(str);
        }

        public void DebugPrint()
        {
            Console.WriteLine("STR="+str+" : Type"+tokenType.ToString());
        }

    }
}
