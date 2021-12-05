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
        public async Task<Album> Create(Album album)
        {
            return await _repository.Create(album);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<bool> Edit(Album album)
        {
            return await _repository.Edit(album);
        }

        public async Task<Album> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Album>> List(string title, string artistName)
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
            return await _repository.Find(parameters);
        }
    }
}
