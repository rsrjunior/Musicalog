using Musicalog.Core.Interfaces;
using Musicalog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Musicalog.Core.Services
{
    public class AlbumService : IAlbumService
    {
        private IMusicalogRepository<Album> _repository;
        public AlbumService(IMusicalogRepository<Album> repository)
        {
            _repository = repository;
        }
        public Album Create(Album album)
        {
            return _repository.Create(album);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public bool Edit(Album album)
        {
            return _repository.Edit(album);
        }

        public Album GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IList<Album> List(string title, string artistName)
        {
            Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>(2);
            if (!string.IsNullOrEmpty(title))
            {
                parameters.Add(nameof(Album.Title), title);
            }
            if (!string.IsNullOrEmpty(artistName))
            {
                parameters.Add(nameof(Album.ArtistName), artistName);
            }
            return _repository.Find(parameters);
        }
    }
}
