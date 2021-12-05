using Musicalog.Core.Entities;
using System;
using System.Collections.Generic;

namespace Musicalog.Core.Interfaces
{
    public interface IAlbumService
    {
        Album Create(Album album);
        bool Delete(int id);
        bool Edit(Album album);
        Album GetById(int id);
        IEnumerable<Album> List(string title, string artistName);
    }
}
