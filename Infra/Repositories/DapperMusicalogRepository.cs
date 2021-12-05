using Dapper;
using Musicalog.Core.Entities;
using Musicalog.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicalog.Infra.Repositories
{
    public class DapperMusicalogRepository<T> : IMusicalogRepository<T> where T : EntityBase
    {
        IDbConnection _connection;
        public DapperMusicalogRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public T Create(T entity)
        {
            StringBuilder insertCommand = new StringBuilder("INSERT INTO " + typeof(T).Name + "(");
            StringBuilder valuesCommand = new StringBuilder(" VALUES (");
            DynamicParameters parameters = new DynamicParameters();
            var entityProperties = entity.GetType().GetProperties().Where(p => !p.Name.Equals("Id") && p.GetValue(entity) != null);

            for (int i = 0; i < entityProperties.Count(); i++)
            {
                var item = entityProperties.ElementAt(i);

                if (i > 0)
                {
                    insertCommand.Append(",");
                    valuesCommand.Append(",");
                }

                insertCommand.Append(item.Name);
                valuesCommand.Append("@" + item.Name);
                parameters.Add("@" + item.Name, item.GetValue(entity));
            }
            insertCommand.Append(")");
            valuesCommand.Append(");");

            entity.Id = _connection.QuerySingle<int>(insertCommand.ToString() +
                                                    valuesCommand.ToString() +
                                                    "SELECT CAST(SCOPE_IDENTITY() as int)", parameters);

            return entity;
        }

        public bool Delete(int id)
        {
            var deleted = _connection.Execute("Delete from " + typeof(T).Name + " where Id=@Id", new { Id = id });
            return deleted > 0;
        }

        public bool Edit(T entity)
        {
            StringBuilder updateCommand = new StringBuilder("UPDATE " + typeof(T).Name + " SET ");
            DynamicParameters parameters = new DynamicParameters();
            var entityProperties = entity.GetType().GetProperties().Where(p => !p.Name.Equals("Id"));

            for (int i = 0; i < entityProperties.Count(); i++)
            {
                var item = entityProperties.ElementAt(i);

                if (i > 0)
                    updateCommand.Append(",");

                updateCommand.Append(item.Name + "=@"+item.Name); 
                parameters.Add("@" + item.Name, item.GetValue(entity));
            }
            updateCommand.Append(" WHERE Id=@Id");
            parameters.Add("@Id", entity.Id);

            var updated = _connection.Execute(updateCommand.ToString(), parameters);


            return updated > 0;
        }

        public IList<T> Find(IDictionary<string, dynamic> parameters)
        {
            StringBuilder query = new StringBuilder("Select * from " + typeof(T).Name);
            DynamicParameters queryParams = new DynamicParameters();
            if (parameters.Count > 0)
            {

                for (int i = 0; i < parameters.Count; i++)
                {
                    var item = parameters.ElementAt(i);

                    query.Append(i == 0 ? " where " : " and ");
                    query.Append(item.Key);
                    query.Append(" = @" + item.Key);
                    queryParams.Add("@" + item.Key, item.Value);
                }
            }
            var result = _connection.Query<T>(query.ToString(), queryParams);
            return result.ToList();
        }

        public IList<T> GetAll()
        {
            return _connection.Query<T>("Select * from " + typeof(T).Name).ToList();
        }

        public T GetById(int id)
        {
            return _connection.Query<T>("Select * from " + typeof(T).Name + " where Id=@Id", new { Id = id }).FirstOrDefault();
        }
    }
}
