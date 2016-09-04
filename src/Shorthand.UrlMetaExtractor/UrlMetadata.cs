using System;
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

    public abstract class Player {
        public Int32 Width { get; internal set; }
        public Int32 Height { get; internal set; }
        public string PlayerUrl { get; internal set; }
        public Enum Type { get; protected set; }
    }

    public enum PlayerType {
        Audio,
        Video
    }

    public class AudioPlayer : Player {
        public string ArtistName { get; internal set; }
        public string SourceUrl { get; internal set; }

        public AudioPlayer() {
            Type = PlayerType.Audio;
        }
    }
}