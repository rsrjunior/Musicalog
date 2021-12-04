using Musicalog.Core.Interfaces;
using Musicalog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Musicalog.Infra.Repositories
{
    public abstract class InMemoryMusicalogRepository<T> : IMusicalogRepository<T> where T : EntityBase
    {
        private IList<T> _inMemoryList;
        private int _index;
        public InMemoryMusicalogRepository(IList<T> inMemoryList)
        {
            _inMemoryList = inMemoryList;
            _index = inMemoryList.Count;
        }
        public T Create(T entity)
        {
            entity.Id = ++_index;
            _inMemoryList.Add(entity);
            return entity;
        }

        public bool Delete(int id)
        {
            var item = _inMemoryList.Where(x => x.Id == id).FirstOrDefault();
            return _inMemoryList.Remove(item);
        }

        public bool Edit(T entity)
        {
            var item = _inMemoryList.Where(x => x.Id == entity.Id).FirstOrDefault();
            if (item==null)
            {
                return false;
            }
            int i = _inMemoryList.IndexOf(item);
            _inMemoryList[i] = entity;
            return true;
        }

        public IList<T> GetAll()
        {
            return _inMemoryList.ToList();
        }

        public T GetById(int id)
        {
            return _inMemoryList.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
