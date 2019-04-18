using System.Threading.Tasks;
using Xunit;

namespace Shorthand.UrlMetaExtractor {
    public class IntegrationTests {
        [Fact]
        public async Task GoodReadsFinalEmpire() {
            var extractor = new MetaExtractor();

            var meta = await extractor.Extract("https://www.goodreads.com/book/show/68428.The_Final_Empire");

            Assert.Equal("The Final Empire (Mistborn, #1)", meta.Title);
            Assert.Equal("https://www.goodreads.com/work/best_book/66322-mistborn-the-final-empire", meta.Url);
            Assert.Contains("9780765311788", meta.ISBN);
            Assert.Equal("www.goodreads.com", meta.Host);
        }

        [Fact]
        public async Task ImdbFrozen() {
            var extractor = new MetaExtractor();

            var meta = await extractor.Extract("http://www.imdb.com/title/tt2294629/");

            Assert.Equal("Frozen (2013) - IMDb", meta.Title);
            Assert.Equal("http://www.imdb.com/title/tt2294629/", meta.Url);
            Assert.Equal("IMDb", meta.SiteName);
            Assert.Equal("www.imdb.com", meta.Host);
        }

        [Fact]
        public async Task BoktipsetMaryJonesHistoria() {
            var extractor = new MetaExtractor();

            var meta = await extractor.Extract("https://www.boktipset.se/bok/mary-jones-historia-nedtecknad-av-mej-sjalv-och-alldeles-uppriktig-om-mitt-liv-samt-om-dolores--john-silver-sa-som-jag-fatt-det-berattat-for-mej-av-dom-sjalva-");

            Assert.Equal("Mary Jones historia: Nedtecknad av mej själv och alldeles uppriktig. Om mitt liv samt om Dolores & John Silver så som jag fått det berättat för mej av dom själva.", meta.Title);
            Assert.Equal("https://www.boktipset.se/bok/mary-jones-historia-nedtecknad-av-mej-sjalv-och-alldeles-uppriktig-om-mitt-liv-samt-om-dolores--john-silver-sa-som-jag-fatt-det-berattat-for-mej-av-dom-sjalva-", meta.Url);
            Assert.Equal("Mary Jones historia: Nedtecknad av mej själv och alldeles uppriktig. Om mitt liv samt om Dolores & John Silver så som jag fått det berättat för mej av dom själva. av Boardy, Elin: Kökspigan hos Long John Silver   Mary Jones historia  är berättelsen från Skattkammarön  om Long John Silver, pirater och papegojor, sedd ur kökspigan Marys perspektiv. Det är 1700-tal och en engelsk landsortsflicka tvingas av fattigdom att ge sig ut för att finna en tjänst, hamnar i Bristol och knackar på dörren till krogen Kikaren, som drivs av Dolores och hennes make, ingen mindre än självaste Long John Silver. De ska bli hennes herrefolk och familj. Dolores lär Mary läsa och skriva och det är hon som ger Mary dagboken och uppdraget att teckna deras historia. Mary Jones berättar om sitt liv i dagboken ? hur hon skickades bort av sin syster som inte längre kunde ta hand om alla föräldralösa syskon, hur hon får arbete på Kikaren och så småningom, medan åren går, blir mer som en dotter än en kökspiga för Dolores och Silver. Mary Jones historia  är ett utsnitt ur en stor[...]", meta.Description);
            Assert.Equal("boktipset.se", meta.SiteName);
            Assert.Equal("https://static.boktipset.se/images/content/A/zu/zuk49uhrrCPjbF6OXEA.jpg", meta.Image);
            Assert.Contains("9789146225225", meta.ISBN);
            Assert.Equal("www.boktipset.se", meta.Host);
        }

        [Fact]
        public async Task SpotifyEvergreyTheParadoxOfTheFlame() {
            var extractor = new MetaExtractor();

            var meta = await extractor.Extract("https://open.spotify.com/album/5XdrYbny8uHg0jo0hWZtv2");

            Assert.Equal("The Paradox of the Flame", meta.Title);
            Assert.Equal("https://open.spotify.com/album/5XdrYbny8uHg0jo0hWZtv2", meta.Url);
            Assert.Equal("The Paradox of the Flame, an album by Evergrey on Spotify", meta.Description);
            Assert.Equal("https://i.scdn.co/image/bb24a35b08fd94c2fc8f789e0b77cc7f0065ceb2", meta.Image);
            Assert.Equal("en", meta.Locale);
            Assert.Equal("open.spotify.com", meta.Host);
        }
     }
}