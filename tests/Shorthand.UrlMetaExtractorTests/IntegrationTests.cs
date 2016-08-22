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
            Assert.AreEqual("www.goodreads.com", meta.Host);
        }

        [Test]
        public async Task ImdbFrozen() {
            var extractor = new MetaExtractor();

            var meta = await extractor.Extract("http://www.imdb.com/title/tt2294629/");

            Assert.AreEqual("Frozen (2013)", meta.Title);
            Assert.AreEqual("http://www.imdb.com/title/tt2294629/", meta.Url);
            Assert.AreEqual("IMDb", meta.SiteName);
            Assert.AreEqual("www.imdb.com", meta.Host);
        }

        [Test]
        public async Task BoktipsetMaryJonesHistoria() {
            var extractor = new MetaExtractor();

            var meta = await extractor.Extract("http://www.boktipset.se/bok/mary-jones-historia-nedtecknad-av-mej-sjalv-och-alldeles-uppriktig-om-mitt-liv-samt-om-dolores--john-silver-sa-som-jag-fatt-det-berattat-for-mej-av-dom-sjalva-");

            Assert.AreEqual("Mary Jones historia: Nedtecknad av mej själv och alldeles uppriktig. Om mitt liv samt om Dolores & John Silver så som jag fått det berättat för mej av dom själva.", meta.Title);
            Assert.AreEqual("http://www.boktipset.se/bok/mary-jones-historia-nedtecknad-av-mej-sjalv-och-alldeles-uppriktig-om-mitt-liv-samt-om-dolores--john-silver-sa-som-jag-fatt-det-berattat-for-mej-av-dom-sjalva-", meta.Url);
            Assert.AreEqual("Mary Jones historia: Nedtecknad av mej själv och alldeles uppriktig. Om mitt liv samt om Dolores & John Silver så som jag fått det berättat för mej av dom själva. av Boardy, Elin: Kökspigan hos Long John Silver     Mary Jones historia  är berättelsen från Skattkammarön  om Long John Silver, pirater och papegojor, sedd ur kökspigan Marys perspektiv. Det är[...]", meta.Description);
            Assert.AreEqual("boktipset.se", meta.SiteName);
            Assert.AreEqual("http://www.boktipset.se/images/content/A/zu/zuk49uhrrCPjbF6OXEA.jpg", meta.Image);
            Assert.Contains("9789146225225", meta.ISBN);
            Assert.AreEqual("www.boktipset.se", meta.Host);
        }

        [Test]
        public async Task Aftonbladet() {
            var extractor = new MetaExtractor();

            var meta = await extractor.Extract("http://www.aftonbladet.se");

            Assert.AreEqual("Aftonbladet: Sveriges nyhetskälla och mötesplats", meta.Title);
            Assert.AreEqual("http://www.aftonbladet.se/", meta.Url);
            Assert.AreEqual("Startsidan - De senaste nyheterna på Aftonbladet.se - Sveriges nyhetsportal", meta.Description);
            Assert.IsNull(meta.Image);
            Assert.IsEmpty(meta.ISBN);
            Assert.AreEqual("www.aftonbladet.se", meta.Host);
        }

        [Test]
        public async Task SpotifyEvergreyTheParadoxOfTheFlame() {
            var extractor = new MetaExtractor();

            var meta = await extractor.Extract("https://open.spotify.com/album/5XdrYbny8uHg0jo0hWZtv2");

            Assert.AreEqual("The Paradox of the Flame", meta.Title);
            Assert.AreEqual("https://open.spotify.com/album/5XdrYbny8uHg0jo0hWZtv2", meta.Url);
            Assert.AreEqual("The Paradox of the Flame, an album by Evergrey on Spotify", meta.Description);
            Assert.AreEqual("https://i.scdn.co/image/bb24a35b08fd94c2fc8f789e0b77cc7f0065ceb2", meta.Image);
            Assert.AreEqual("en", meta.Locale);
            Assert.AreEqual("open.spotify.com", meta.Host);
        }
    }
}