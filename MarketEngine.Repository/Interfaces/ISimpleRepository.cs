using MarketEngine.Model.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MarketEngine.Repository.Interfaces
{
    public interface ISimpleRepository<T> where T : ISimpleModel
    {
        T GetById(string id);
        T GetByName(string name);
        T Create(T t);
        void Delete(T t);
        void Delete(string id);
        void DeleteAll();
        void Update(T t);
        T FindOne(string fieldName, object fieldValue);
        T FindOne(IDictionary<string, object> fieldValues);
        IList<T> FindAll(string fieldName, object fieldValue);
        IList<T> GetAll();
        long Count();
        void CleanUp();
        T UpdateSpecificAttribute(string id, Expression<Func<T, object>> memberExpression, object value);
        void ForEach(Action<T> itemAction);
        IList<string> GetAllIds();
        void CreateOrUpdate(T t);
        T GetByPropertyName(string propertyName, object propertyValue);
        T CreateWithId(T t);
        object GetByIdAsDocument(string id);
        bool ContainsProperty(string id, string propPath);
        bool Exists(string id);
        long GetCount();
        T GetBy(Expression<Func<T, bool>> filter);
        IList<T> ListBy(Expression<Func<T, bool>> filter);
    }
}
