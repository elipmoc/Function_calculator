using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Function_calculator;

namespace LexicalAnalyzerTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(CreateAST.CreateSikiAST(LexicalAnalyzer.Lexicalanalysis("4+5")).GetValue(), 4 + 5);
            Assert.AreEqual(CreateAST.CreateSikiAST(LexicalAnalyzer.Lexicalanalysis("((4)+5)")).GetValue(), 4 + 5);
            Assert.AreEqual(CreateAST.CreateSikiAST(LexicalAnalyzer.Lexicalanalysis("(4+3)*7")).GetValue(), (4 + 3) * 7);
            Assert.AreEqual( CreateAST.CreateSikiAST(LexicalAnalyzer.Lexicalanalysis("7/3/7/3")).GetValue(), 7 / 3 / 7 / 3);
        }
    }
}
