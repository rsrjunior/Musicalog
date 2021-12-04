﻿using Musicalog.Core.Interfaces;
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
        private IAlbumRepository _repository;
        public AlbumService(IAlbumRepository repository)
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

        public IList<Album> Filter(string title, string artistName)
        {
            return _repository.Filter(title, artistName);
        }
    }
}