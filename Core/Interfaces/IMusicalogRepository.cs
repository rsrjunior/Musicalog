using Musicalog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Musicalog.Core.Interfaces
{
    public interface IMusicalogRepository<T> where T : EntityBase
    {
        public T Create(T entity);
        public bool Delete(int id);
        public bool Edit(T entity);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(IDictionary<string,dynamic> parameters);
        T GetById(int id);
    }
}
