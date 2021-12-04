using Xunit;
using Musicalog.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Musicalog.Core.Entities;
using Musicalog.Core.Enums;
using Musicalog.Infra.Repositories;

namespace Musicalog.Core.Services.Tests
{
    public class AlbumServiceTests
    {
        [Fact()]
        public void AlbumServiceTest()
        {
            var albumList = new List<Album>();
    
            var albumService = new AlbumService(new InMemoryAlbumRepository(albumList));

            Assert.NotNull(albumService);
        }

        [Fact()]
        public void CreateTest()
        {
            var albumService = new AlbumService(new InMemoryAlbumRepository(new List<Album>()));

            var newItem = albumService.Create(new Album { Id = 1, ArtistName = "Artist1", Title = "Title1", Stock = 1, Type = AlbumType.CD });

            Assert.NotNull(newItem);
        }

        [Fact()]
        public void DeleteTest()
        {
            var albumList = new List<Album>();
            albumList.Add(new Album { Id = 1, ArtistName = "Artist1", Title = "Title1", Stock = 1, Type = AlbumType.CD });

            var albumService = new AlbumService(new InMemoryAlbumRepository(albumList));

            Assert.True(albumService.Delete(albumList[0].Id));
        }

        [Fact()]
        public void EditTest()
        {
            var albumList = new List<Album>();
            var item = new Album { Id = 1, ArtistName = "Artist1", Title = "Title1", Stock = 1, Type = AlbumType.CD };
            albumList.Add(item);
            var albumService = new AlbumService(new InMemoryAlbumRepository(albumList));

            item.Title = "ChangedTitle";

            Assert.True(albumService.Edit(item));
            Assert.Equal(item.Title, albumService.GetById(item.Id).Title);
        }

        [Fact()]
        public void GetByIdTest()
        {
            var albumList = new List<Album>();
            var item = new Album { Id = 1, ArtistName = "Artist1", Title = "Title1", Stock = 1, Type = AlbumType.CD };
            albumList.Add(item);
            var albumService = new AlbumService(new InMemoryAlbumRepository(albumList));

            var getItem = albumService.GetById(item.Id);

            Assert.NotNull(getItem);
            Assert.Equal(item.Id, getItem.Id);
        }

        [Fact()]
        public void ListTest()
        {
            var albumList = new List<Album>();
            albumList.Add(new Album { Id = 1, ArtistName = "Artist1", Title = "Title1", Stock = 1, Type = AlbumType.CD });

            var albumService = new AlbumService(new InMemoryAlbumRepository(albumList));

            Assert.NotEmpty(albumService.Filter("", ""));
        }
        [Theory]
        [InlineData("Title1", "Artist1")]
        [InlineData("Title1", "")]
        [InlineData("", "Artist1")]
        public void ListFilteredTest(string title, string artistName)
        {
            var albumList = new List<Album>();
            albumList.Add(new Album { Id = 1, ArtistName = artistName, Title = title, Stock = 1, Type = AlbumType.CD });
            albumList.Add(new Album { Id = 2, ArtistName = "AnotherArtist", Title = "AnotherTitle", Stock = 1, Type = AlbumType.CD });

            var albumService = new AlbumService(new InMemoryAlbumRepository(albumList));

            Assert.Equal(1, albumService.Filter(title, artistName).Count);
        }
    }
}