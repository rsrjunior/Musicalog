using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musicalog.Core;
using Musicalog.Core.Services;
using WebApi.Models;
using Musicalog.Core.Entities;
using Musicalog.Core.Enums;
using Musicalog.Core.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        IAlbumService _albumService;
        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public IEnumerable<AlbumDTO> Get(string title, string artistName)
        {
            var albums = _albumService.List(title, artistName);
            return albums.Select(i => new AlbumDTO
            {
                Id = i.Id,
                ArtistName = i.ArtistName,
                Stock = i.Stock,
                Title = i.Title,
                Type = i.Type.ToString()
            });
        }

        [HttpGet("{id}")]
        public AlbumDTO Get(int id)
        {
            var album = _albumService.GetById(id);
            if (album == null)
            {
                return null;
            }

            return new AlbumDTO
            {
                Id = album.Id,
                ArtistName = album.ArtistName,
                Stock = album.Stock,
                Title = album.Title,
                Type = album.Type.ToString()
            };
        }

        [HttpPost]
        public AlbumDTO Post([FromBody] AlbumDTO item)
        {
            var album = _albumService.Create(new Album
            {
                ArtistName = item.ArtistName,
                Title = item.Title,
                Stock = item.Stock,
                Type = Enum.Parse<AlbumType>(item.Type)
            });

            return new AlbumDTO
            {
                Id = album.Id,
                ArtistName = album.ArtistName,
                Stock = album.Stock,
                Title = album.Title,
                Type = album.Type.ToString()
            };
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] AlbumDTO item)
        {
            var album = _albumService.GetById(id);
            if (album == null)
            {
                throw new KeyNotFoundException();
            }

            album.ArtistName = item.ArtistName;
            album.Title = item.Title;
            album.Stock = item.Stock;
            album.Type = Enum.Parse<AlbumType>(item.Type);

            _albumService.Edit(album);

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (!_albumService.Delete(id))
            {
                throw new Exception();
            }
        }
    }
}
