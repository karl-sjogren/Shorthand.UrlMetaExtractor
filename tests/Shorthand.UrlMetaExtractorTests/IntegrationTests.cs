using System.Threading.Tasks;
using NUnit.Framework;

namespace Shorthand.UrlMetaExtractor {
    [TestFixture]
    public class IntegrationTests {
        [Test]
        public async Task GoodReadsFinalEmpire() {
            var extractor = new MetaExtractor();

            var meta = await extractor.Extract("http://www.goodreads.com/book/show/68428.The_Final_Empire");

            Assert.AreEqual("The Final Empire (Mistborn, #1)", meta.Title);
            Assert.AreEqual("http://www.goodreads.com/work/best_book/66322-mistborn-the-final-empire", meta.Url);
            Assert.Contains("076531178X", meta.ISBN);
        }

        [Test]
        public async Task ImdbFrozen() {
            var extractor = new MetaExtractor();

            var meta = await extractor.Extract("http://www.imdb.com/title/tt2294629/");

            Assert.AreEqual("Frozen (2013)", meta.Title);
            Assert.AreEqual("http://www.imdb.com/title/tt2294629/", meta.Url);
            Assert.AreEqual("IMDb", meta.SiteName);
            Assert.Contains("076531178X", meta.ISBN);
        }
    }
}