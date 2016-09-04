using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using AngleSharp.Parser.Html;

namespace Shorthand.UrlMetaExtractor {
    [TestFixture]
    public class ExtractorTests {
        [Test]
        public async Task ExtractOpenGraphTags() {
            var html = File.ReadAllText(Path.Combine("Resources", "OpenGraphTags.html"));

            var parser = new HtmlParser();
            var document = await parser.ParseAsync(html);

            var meta = new UrlMetadata();

            MetaExtractor.ExtractOpenGraph(document, meta);

            Assert.AreEqual("The Rock", meta.Title);
            Assert.AreEqual("http://www.imdb.com/title/tt0117500/", meta.Url);
            Assert.AreEqual("http://ia.media-imdb.com/images/rock.jpg", meta.Image);
            Assert.AreEqual("Directed by Michael Bay.  With Sean Connery, Nicolas Cage, Ed Harris, John Spencer. A mild-mannered chemist and an ex-con must lead the counterstrike when a rogue group of military men, led by a renegade general, threaten a nerve gas attack from Alcatraz against San Francisco.", meta.Description);
            Assert.AreEqual("sv", meta.Locale);
        }

        [Test]
        public async Task ExtractHtmlFallback() {
            var html = File.ReadAllText(Path.Combine("Resources", "HtmlFallback.html"));

            var parser = new HtmlParser();
            var document = await parser.ParseAsync(html);

            var meta = new UrlMetadata();

            MetaExtractor.ExtractHtmlMeta(document, meta);

            Assert.AreEqual("The Rock", meta.Title);
            Assert.AreEqual("http://ia.media-imdb.com/images/rock.jpg", meta.Image);
            Assert.AreEqual("Directed by Michael Bay.  With Sean Connery, Nicolas Cage, Ed Harris, John Spencer. A mild-mannered chemist and an ex-con must lead the counterstrike when a rogue group of military men, led by a renegade general, threaten a nerve gas attack from Alcatraz against San Francisco.", meta.Description);
            Assert.AreEqual("sv", meta.Locale);
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

        [Test]
        public async Task ExtractTwitterTags() {
            var html = File.ReadAllText(Path.Combine("Resources", "TwitterTags.html"));

            var parser = new HtmlParser();
            var document = await parser.ParseAsync(html);

            var meta = new UrlMetadata();

            MetaExtractor.ExtractTwitterTags(document, meta);

            Assert.AreEqual("DeejaySample", meta.Title);
            Assert.AreEqual("https://i1.sndcdn.com/avatars-000127452541-ap7j7n-t500x500.jpg", meta.Image);
            Assert.AreEqual("SoundCloud", meta.SiteName);
        }
    }
}
