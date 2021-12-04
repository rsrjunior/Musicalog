using System;

namespace WebApi.Models
{
    public class AlbumDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ArtistName { get; set; }
        public string Type { get; set; }
        public int Stock { get; set; }
    }
}