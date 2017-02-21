using System;
using System.Linq;
using System.Linq.Expressions;
using ArthPWA.Common.CrudBase;

namespace ArthPWA.DataAccess
{
    //marker interface for repositories
    public interface IRepository
    {
    }

    public interface IRepository<TResource> : IRepository
        where TResource : ResourceWithId
    {
        TResource GetResource(string id, params Expression<Func<TResource, object>>[] explicitPropertyList);
        IQueryable<TResource> GetResources();
        string CreateResource(TResource resource);
        void UpdateResource(TResource resource, params Expression<Func<TResource, object>>[] explicitPropertyList);
        void Delete(string id);
    }

    public interface IRepositoryFactory
    {
        TRepository InstanceOf<TRepository>()
            where TRepository : IRepository;
    }
}