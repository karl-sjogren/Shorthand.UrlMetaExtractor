using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;

namespace Shorthand.UrlMetaExtractor {
    public class MetaExtractor {
        private readonly HttpMessageHandler _messageHandler;

        public MetaExtractor() {

        }

        internal MetaExtractor(HttpMessageHandler messageHandler) {
            _messageHandler = messageHandler;
        }

        public async Task<UrlMetadata> Extract(string uri) {
            return await Extract(new Uri(uri));
        }

        public async Task<UrlMetadata> Extract(Uri uri) {
            var client = new HttpClient(_messageHandler ?? new HttpClientHandler());

            var responseHtml = await client.GetStringAsync(uri);

            var document = await ParseDocument(responseHtml);


            var meta = new UrlMetadata();

            ExtractOpenGraph(document, meta);
            ExtractMisc(document, meta);

            return meta;
        }

        internal static void ExtractOpenGraph(IHtmlDocument document, UrlMetadata meta) {
            var ogTags = document.QuerySelectorAll("meta[property^='og:']").Select(t => new KeyValuePair<string, string>(t.Attributes["property"].Value.ToLowerInvariant(), t.Attributes["content"].Value));

            foreach(var ogTag in ogTags) {
                switch(ogTag.Key) {
                    case "og:title":
                        meta.SetProperty(m => m.Title, ogTag.Value);
                        break;
                    case "og:url":
                        meta.SetProperty(m => m.Url, ogTag.Value);
                        break;
                    case "og:image":
                        meta.SetProperty(m => m.Image, ogTag.Value);
                        break;
                    case "og:site_name":
                        meta.SetProperty(m => m.SiteName, ogTag.Value);
                        break;
                    case "og:description":
                        meta.SetProperty(m => m.Description, ogTag.Value);
                        break;
                }
            }
        }

        internal static void ExtractMisc(IHtmlDocument document, UrlMetadata meta) {
            var ogTags = document.QuerySelectorAll("meta[property]").Select(t => new KeyValuePair<string, string>(t.Attributes["property"].Value.ToLowerInvariant(), t.Attributes["content"].Value));

            foreach(var ogTag in ogTags) {
                switch(ogTag.Key) {
                    case "book:isbn":
                    case "good_reads:isbn":
                        meta.PushProperty(m => m.ISBN, ogTag.Value);
                        break;
                }
            }
        }

        private static async Task<IHtmlDocument> ParseDocument(string responseHtml) {
            var parser = new HtmlParser();
            var document = await parser.ParseAsync(responseHtml);
            return document;
        }
    }
}
