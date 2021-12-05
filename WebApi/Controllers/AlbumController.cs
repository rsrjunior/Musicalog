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
        public ActionResult<IEnumerable<AlbumModel>> Get(string title, string artistName)
        {
            try
            {
                var albums = _albumService.List(title, artistName);
                return Ok(albums.Select(i => new AlbumModel
                {
                    Id = i.Id,
                    ArtistName = i.ArtistName,
                    Stock = i.Stock,
                    Title = i.Title,
                    Type = i.Type.ToString()
                }));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public ActionResult<AlbumModel> Get(int id)
        {
            try
            {
                var album = _albumService.GetById(id);
                if (album == null)
                {
                    return NotFound();
                }

                return Ok(new AlbumModel
                {
                    Id = album.Id,
                    ArtistName = album.ArtistName,
                    Stock = album.Stock,
                    Title = album.Title,
                    Type = album.Type.ToString()
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
           
        }

        [HttpPost]
        public ActionResult<AlbumModel> Post([FromBody] AlbumModel item)
        {
            try
            {
                var album = _albumService.Create(new Album
                {
                    ArtistName = item.ArtistName,
                    Title = item.Title,
                    Stock = item.Stock,
                    Type = Enum.Parse<AlbumType>(item.Type)
                });

                return Ok(new AlbumModel
                {
                    Id = album.Id,
                    ArtistName = album.ArtistName,
                    Stock = album.Stock,
                    Title = album.Title,
                    Type = album.Type.ToString()
                });
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] AlbumModel item)
        {
            try
            {
                var album = _albumService.GetById(id);
                if (album == null)
                {
                    return NotFound();
                }

                album.ArtistName = item.ArtistName;
                album.Title = item.Title;
                album.Stock = item.Stock;
                album.Type = Enum.Parse<AlbumType>(item.Type);

                bool result = _albumService.Edit(album);

                return result ? Ok() : Problem("The resource was not changed");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                bool result = _albumService.Delete(id);
                return result ? Ok() : Problem("The resource was not deleted");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
