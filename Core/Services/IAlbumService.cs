using Musicalog.Core.Entities;
using System;
using System.Collections.Generic;

namespace Musicalog.Core.Services
{
    public interface IAlbumService
    {
        Album Create(Album album);
        bool Delete(int id);
        bool Edit(Album album);
        Album GetById(int id);
        IList<Album> Filter(string title, string artistName);
    }
}
