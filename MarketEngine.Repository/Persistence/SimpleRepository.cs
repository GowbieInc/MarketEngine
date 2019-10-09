using MarketEngine.Model.Interfaces.Models;
using MarketEngine.Repository.Interfaces;
using MarketEngine.Repository.RepositoryConnection;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MarketEngine.Repository
{
    public abstract class SimpleRepository<T> : MongoDBRepository, ISimpleRepository<T> where T : ISimpleModel
    {
        protected SimpleRepository()
        {
        }

        public object GetByIdAsDocument(string id)
        {
            var mongoQuery = Query<T>.EQ(u => u.Id, id);

            return BaseCollection.FindOneAs<BsonDocument>(mongoQuery);
        }

        public bool Exists(string id)
        {
            var mongoQuery = Query<T>.EQ(u => u.Id, id);

            return BaseCollection.Count(mongoQuery) > 0;
        }

        protected virtual string GetCollectionName()
        {
            var collectionName = typeof(T).Name + "s";
            collectionName = collectionName.Replace("ys", "ies");
            if (collectionName.EndsWith("chs"))
            {
                collectionName = collectionName.Substring(0, collectionName.Length - 3) + "ches";
            }
            return collectionName;
        }

        protected MongoCollection<T> BaseCollection
        {
            get
            {
                var collectionName = GetCollectionName();
                return (MongoCollection<T>)Database.GetCollection<T>(collectionName);
            }
        }

        public T GetById(string id)
        {
            var mongoQuery = Query<T>.EQ(u => u.Id, id);
            return BaseCollection.FindOne(mongoQuery);
        }

        public T GetByName(string name)
        {
            return BaseCollection.FindOne(Query<T>.EQ(u => u.Name, name));
        }

        public virtual T Create(T t)
        {
            var retry = 0;
            do
            {
                try
                {
                    t.Id = Guid.NewGuid().ToString();
                    BaseCollection.Insert(t);
                    break;
                }
                catch (MongoDuplicateKeyException e)
                {
                    retry++;
                    if (retry > 3)
                        throw new InvalidOperationException($"Maximum of {retry} retries exceeded while creating {typeof(T).Name}: {e}");
                }
            } while (true);

            return t;
        }

        public virtual T CreateWithId(T t)
        {
            if (string.IsNullOrEmpty(t.Id))
                t.Id = Guid.NewGuid().ToString();

            BaseCollection.Insert(t);

            return t;
        }
        public void Delete(T t)
        {
            BaseCollection.Remove(Query<T>.EQ(u => u.Id, t.Id));
        }

        public void Delete(string id)
        {
            BaseCollection.Remove(Query<T>.EQ(u => u.Id, id));
        }

        public void DeleteAll()
        {
            BaseCollection.RemoveAll();
        }

        public virtual void Update(T t)
        {
            BaseCollection.Save(t);
        }

        public T FindOne(string fieldName, object fieldValue)
        {
            return BaseCollection.FindOne(Query.EQ(fieldName, BsonValue.Create(fieldValue)));
        }

        public T FindOne(IDictionary<string, object> fieldValues)
        {
            return BaseCollection.FindOne(new QueryDocument(fieldValues));
        }

        public IList<T> FindAll(string fieldName, object fieldValue)
        {
            return BaseCollection.Find(Query.EQ(fieldName, BsonValue.Create(fieldValue))).ToList();
        }

        public IList<T> GetAll()
        {
            return BaseCollection.FindAll().OrderBy(t => t.Name).ToList();
        }

        public long Count()
        {
            return BaseCollection.Count();
        }

        public void CleanUp()
        {
            BaseCollection.RemoveAll();
        }

        public virtual T UpdateSpecificAttribute(string id, Expression<Func<T, object>> memberExpression, object value)
        {
            UpdateBuilder<T> ub = new UpdateBuilder<T>();

            ub.Set(memberExpression, value);

            BaseCollection.Update(Query<T>.EQ(t => t.Id, id), ub);

            return BaseCollection.FindOneById(id);
        }

        public IList<string> GetAllIds()
        {
            IList<string> result = new List<string>();
            ForEach((a) => result.Add(a.Id));
            return result;
        }

        public void CreateOrUpdate(T t)
        {
            BaseCollection.Save(t);
        }

        public T GetByPropertyName(string propertyName, object propertyValue)
        {
            return BaseCollection.FindOne(Query.EQ(propertyName, BsonValue.Create(propertyValue)));
        }

        public void ForEach(Action<T> itemAction)
        {
            MongoCursor<T> cursor = BaseCollection.FindAll().SetFlags(QueryFlags.NoCursorTimeout).SetSnapshot().SetSortOrder();
            foreach (T item in cursor)
            {
                itemAction(item);
            }
        }

        public bool ContainsProperty(string id, string propPath)
        {
            BsonDocument doc = (BsonDocument)GetByIdAsDocument(id);
            var props = propPath.Split('.');
            var curProp = 0;
            foreach (var prop in props)
            {
                if (!doc.Contains(prop))
                    return false;
                curProp++;
                if (curProp < props.Length)
                {
                    if (doc[prop] is BsonNull)
                        return false;
                    doc = doc[prop].AsBsonDocument;
                }

            }
            return true;
        }

        public long GetCount()
        {
            return BaseCollection.Count();
        }

        public T GetBy(Expression<Func<T, bool>> filter)
        {
            return Queryable.Where(BaseCollection.AsQueryable(), filter).FirstOrDefault();
        }

        public IList<T> ListBy(Expression<Func<T, bool>> filter)
        {
            return Queryable.Where(BaseCollection.AsQueryable(), filter).ToList();
        }
    }
}
