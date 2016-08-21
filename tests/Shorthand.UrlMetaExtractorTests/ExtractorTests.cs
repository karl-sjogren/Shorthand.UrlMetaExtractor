using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using AngleSharp.Parser.Html;

namespace Shorthand.UrlMetaExtractor {
    [TestFixture]
    public class ExtractorTests {

        [Test]
        public async Task ExtractBasicTags() {
            var html = File.ReadAllText(Path.Combine("Resources", "BasicTags.html"));

            var parser = new HtmlParser();
            var document = await parser.ParseAsync(html);

            var meta = new UrlMetadata();

            MetaExtractor.ExtractOpenGraph(document, meta);

            Assert.AreEqual("The Rock", meta.Title);
            Assert.AreEqual("http://www.imdb.com/title/tt0117500/", meta.Url);
            Assert.AreEqual("http://ia.media-imdb.com/images/rock.jpg", meta.Image);
        }

        [Test]
        public async Task ExtractRepeatedTags() {
            var html = File.ReadAllText(Path.Combine("Resources", "RepeatedTags.html"));

            var parser = new HtmlParser();
            var document = await parser.ParseAsync(html);

            var meta = new UrlMetadata();

            MetaExtractor.ExtractOpenGraph(document, meta);

            Assert.AreEqual("Frozen", meta.Title);
            Assert.AreEqual("http://www.imdb.com/title/tt2294629/", meta.Url);
            Assert.AreEqual("http://ia.media-imdb.com/images/frozen.jpg", meta.Image);
        }

        [Test]
        public async Task IsbnArray() {
            var html = File.ReadAllText(Path.Combine("Resources", "ISBN.html"));

            var parser = new HtmlParser();
            var document = await parser.ParseAsync(html);

            var meta = new UrlMetadata();

            MetaExtractor.ExtractMisc(document, meta);

            Assert.AreEqual(3, meta.ISBN.Count);
            Assert.Contains("076531178X", meta.ISBN);
            Assert.Contains("9780765356130", meta.ISBN);
            Assert.Contains("9780575089945", meta.ISBN);
        }
    }
}
