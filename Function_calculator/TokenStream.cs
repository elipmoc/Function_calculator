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

        //次のトークンへインデックスを進める
        public void Next() { NowIndex++; }

        //前のトークンへインデックスを戻す
        public void Prev() { NowIndex--; }

        //チェックポイントを設置
        public void SetCheckPoint()
        {
            checkPoint = NowIndex;
        }

        //チェックポイントを設置したインデックスにロールバック
        public void Rollback()
        {
            NowIndex = checkPoint;
        }

        //nowindexがさすトークンを得る
        internal Token Get() { return tokenlist[NowIndex]; }

        internal Token this[int index]
        {
            get { return tokenlist[index];  }
        }

        public int Size { get { return tokenlist.Count; } }

        internal TokenStream(List<Token> tokenlist)
        {
            NowIndex = 0;
            this.tokenlist = tokenlist;
        }
    }
}
