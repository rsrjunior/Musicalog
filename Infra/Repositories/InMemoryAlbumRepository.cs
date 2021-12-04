using Musicalog.Core.Entities;
using Musicalog.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicalog.Infra.Repositories
{
    public class InMemoryAlbumRepository: InMemoryMusicalogRepository<Album>, IAlbumRepository
    {
        private IList<Album> _inMemoryList;
        public InMemoryAlbumRepository(IList<Album> inMemoryList) : base(inMemoryList) {
            _inMemoryList = inMemoryList;
        }

        public IList<Album> Filter(string title, string artistName)
        {
            return _inMemoryList.Where(x =>
                 (string.IsNullOrEmpty(title) || x.Title.Equals(title)) &&
                 (string.IsNullOrEmpty(artistName) || x.ArtistName.Equals(artistName))).ToList();
        }
    }
}
