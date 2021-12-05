using Musicalog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Musicalog.Core.Interfaces
{
    public interface IAlbumService
    {
        Task<Album> Create(Album album);
        Task<bool> Delete(int id);
        Task<bool> Edit(Album album);
        Task<Album> GetById(int id);
        Task<IEnumerable<Album>> List(string title, string artistName);
    }
}
