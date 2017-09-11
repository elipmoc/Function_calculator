using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function_calculator
{
   public class TokenStream
    {
        private readonly List<Token> tokenlist = new List<Token>();
        public int NowIndex { get; private set; }
        private int checkPoint = 0;

        public void Next() { NowIndex++; }
        public void Prev() { NowIndex--; }
        public void SetCheckPoint()
        {
            checkPoint = NowIndex;
        }

        public void Rollback()
        {
            NowIndex = checkPoint;
        }

        internal Token Get() { return tokenlist[NowIndex]; }

        public int Size { get { return tokenlist.Count; } }

        internal TokenStream(List<Token> tokenlist)
        {
            NowIndex = 0;
            this.tokenlist = tokenlist;
        }
    }
}
