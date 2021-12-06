using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using Musicalog.Core.Entities;
using Musicalog.Core.Enums;
using Musicalog.Core.Interfaces;
using AutoMapper;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        IAlbumService _albumService;
        IMapper _mapper;
        public AlbumController(IAlbumService albumService, IMapper mapper)
        {
            _albumService = albumService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumModel>>> Get(string title, string artistName)
        {
            try
            {
                var albums = await _albumService.List(title, artistName);
                return Ok(albums.Select(a => _mapper.Map<AlbumModel>(a)));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumModel>> Get(int id)
        {
            try
            {
                var album = await _albumService.GetById(id);
                if (album == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<AlbumModel>(album));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
           
        }

        [HttpPost]
        public async Task<ActionResult<AlbumModel>> Post([FromBody] AlbumModel item)
        {
            try
            {
                var album = await _albumService.Create(_mapper.Map<Album>(item));

                return Ok(_mapper.Map<AlbumModel>(album));
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AlbumModel item)
        {
            try
            {
                var album = await _albumService.GetById(id);
                if (album == null)
                {
                    return NotFound();
                }
                item.Id = album.Id;

                var changedAlbum = _mapper.Map<Album>(item);

                bool result = await _albumService.Edit(changedAlbum);

                return result ? Ok() : Problem("The resource was not changed");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                bool result = await _albumService.Delete(id);
                return result ? Ok() : Problem("The resource was not deleted");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
