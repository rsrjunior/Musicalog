using Musicalog.Core.Enums;

namespace Musicalog.Core.Entities
{
    public class Album: EntityBase
    {
        public string Title { get; set; }
        public string ArtistName { get; set; }
        public AlbumType Type{ get; set; }
        public int Stock { get; set; }
    }
}
