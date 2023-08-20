using Newtonsoft.Json;

namespace ArtistMusic.Models
{
    public class MusicBrainzArtistResponse
    {
        [JsonProperty("artists")]
        public List<MusicBrainzArtist> Artists { get; set; }
    }

    public class MusicBrainzArtist
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    //public class MusicBrainzReleaseResponse
    //{
    //    [JsonProperty("releases")]
    //    public List<MusicBrainzRelease> Releases { get; set; }
    //}

    //public class MusicBrainzRelease
    //{
    //    // Define properties for album information
    //}

    public class Song
    {
        public string Title { get; set; }
        public string AlbumTitle { get; set; }
    }


    public class MusicBrainzReleaseResponse
    {
        [JsonProperty("releases")]
        public List<MusicBrainzRelease> Releases { get; set; }
    }

    public class MusicBrainzRelease
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("media")]
        public List<MusicBrainzMedium> Media { get; set; }
    }

    public class MusicBrainzMedium
    {
        [JsonProperty("tracks")]
        public List<MusicBrainzTrack> Tracks { get; set; }
    }

    public class MusicBrainzTrack
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }


}
