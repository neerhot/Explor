using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
 
using Newtonsoft.Json;

using ArtistMusic.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArtistMusic.Controllers
{
    public class ArtistSearchController : Controller
    {
        private const string MusicBrainzApiUrl = "https://musicbrainz.org/ws/2/";
        private const string UserAgent = "YourApp/1.0";

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> SearchResults(string artistName)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", UserAgent);

                    var response = await client.GetStringAsync($"{MusicBrainzApiUrl}artist/?query=artist:{artistName}&fmt=json");
                    var artists = JsonConvert.DeserializeObject<MusicBrainzArtistResponse>(response);

                    if (artists.Artists.Count == 0)
                    {
                        ViewBag.ErrorMessage = "Artist not found.";
                        return View();
                    }

                    var artistId = artists.Artists[0].Id;
                    var releaseResponse = await client.GetStringAsync($"{MusicBrainzApiUrl}release/?artist={artistId}&fmt=json");
                    var releases = JsonConvert.DeserializeObject<MusicBrainzReleaseResponse>(releaseResponse);

                    var songs = new List<Song>();

                    foreach (var release in releases.Releases)
                    {
                        var albumTitle = release.Title;

                        // Handle the case when a release has no tracks
                        if (release.Media == null)
                        {
                            songs.Add(new Song
                            {
                                Title = "No tracks available",
                                AlbumTitle = albumTitle
                            });
                        }
                        else
                        {
                            foreach (var medium in release.Media)
                            {
                                foreach (var track in medium.Tracks)
                                {
                                    songs.Add(new Song
                                    {
                                        Title = track.Title,
                                        AlbumTitle = albumTitle
                                    });
                                }
                            }
                        }
                    }

                    ViewBag.ArtistName = artistName;
                    ViewBag.Songs = songs;

                    return View(songs);

                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred.";
                return View();
            }
        }
    }
}
