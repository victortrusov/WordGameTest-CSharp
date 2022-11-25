namespace WordGameTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WordGame;

    [TestClass]
    public class WordGameServiceTest
    {
        private IValidWords validWords = new ValidWords();
        private WordGameService wordGameService;

        [TestInitialize]
        public void Initialize()
        {
            this.wordGameService = new WordGameService("areallylongword", this.validWords);
        }

        [TestMethod]
        public void TestSubmissions()
        {
            Assert.AreEqual(3, this.wordGameService.SubmitWord("player1", "all"));
            Assert.AreEqual(4, this.wordGameService.SubmitWord("player4", "ally"));
            Assert.AreEqual(6, this.wordGameService.SubmitWord("player5", "woolly"));
            Assert.AreEqual(4, this.wordGameService.SubmitWord("player2", "word"));
            Assert.AreEqual(null, this.wordGameService.SubmitWord("player3", "tale"));
            Assert.AreEqual(null, this.wordGameService.SubmitWord("player4", "glly"));
            Assert.AreEqual(null, this.wordGameService.SubmitWord("player6", "adder"));

            Assert.AreEqual(null, this.wordGameService.GetPlayerNameAtPosition(-1));
            Assert.AreEqual("player5", this.wordGameService.GetPlayerNameAtPosition(0));
            Assert.AreEqual("player4", this.wordGameService.GetPlayerNameAtPosition(1));
            Assert.AreEqual("player2", this.wordGameService.GetPlayerNameAtPosition(2));
            Assert.AreEqual("player1", this.wordGameService.GetPlayerNameAtPosition(3));
            Assert.AreEqual(null, this.wordGameService.GetPlayerNameAtPosition(11));
        }
    }
}
