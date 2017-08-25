using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function_calculator
{
    class TokenStream
    {
        private readonly List<Token> tokenlist = new List<Token>();
        public int nowIndex = 0;

        public Token this[int index]
        {
            get { return tokenlist[index];  }
        }

        public int Size { get { return tokenlist.Count; } }

        public TokenStream(List<Token> tokenlist)
        {
            this.tokenlist = tokenlist;
        }
    }
}
