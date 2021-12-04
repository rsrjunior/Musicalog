using Musicalog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicalog.Core.Interfaces
{
    public interface IAlbumRepository : IMusicalogRepository<Album>
    {
        IList<Album> Filter(string title, string artistName);
    }
}
