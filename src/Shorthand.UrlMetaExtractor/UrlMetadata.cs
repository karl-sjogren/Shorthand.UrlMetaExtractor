using System.Collections.Generic;

namespace Shorthand.UrlMetaExtractor {
    public class UrlMetadata {
        public string Title { get; internal set; }
        public string SiteName { get; internal set; }
        public string Description { get; internal set; }
        public string Image { get; internal set; }
        public string Url { get; internal set; }
        public string Host { get; internal set; }
        public string Locale { get; internal set; }
        // ReSharper disable once InconsistentNaming
        public List<string> ISBN { get; internal set; }

        public UrlMetadata() {
            ISBN = new List<string>();
        }
    }
}