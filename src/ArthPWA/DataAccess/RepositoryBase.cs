using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ArthPWA.Api.Common;
using ArthPWA.Common.CrudBase;
using ArthPWA.Common;
//using CFS.SkyNet.Infrastructure.EventLog;
using ArthPWA.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace ArthPWA.DataAccess
{
    public abstract class RepositoryBase<TResource> : IRepository<TResource> where TResource : ResourceWithId
    {
//        private readonly IEventLogRepository _eventLogRepository;
        protected MongoCollection ResourceCollection { get; private set; }

        /// <summary>
        /// Base class for most database access
        /// </summary>
        /// <param name="database">The database to be used</param>
        /// <param name="collectionName">The name for the collection to be used</param>
        /// <param name="eventLogRepository">collection where crud operations are logged</param>
        protected RepositoryBase(
            IMongoDb database,
            string collectionName,
           // IEventLogRepository eventLogRepository,
            Func<string, MongoCollection> collectionGetter = null)
        {
         //   _eventLogRepository = eventLogRepository;
            if (collectionGetter == null)
            {
                collectionGetter = s => database.Database.GetCollection<TResource>(s);
            }
            ResourceCollection = collectionGetter(collectionName);
        }

        public virtual TResource GetResource(string id, params Expression<Func<TResource, object>>[] explicitPropertyList)
        {
            var mongoQuery = Query.And(
                    Query.NotExists("_deleted"),
                    Query<TResource>.EQ(r => r.Id, id));

            var args = new FindOneArgs
            {
                Query = mongoQuery,
                Fields = Fields<TResource>.Include(explicitPropertyList)
            };

            if (explicitPropertyList.Length > 0)
            {
                //need to add the type discriminator
                args.Fields.ToBsonDocument().Add("_t", 1);
            }
            
            return ResourceCollection.FindOneAs<TResource>(args);
        }

        public virtual IQueryable<TResource> GetResources()
        {
            return ResourceCollection
                .AsQueryable<TResource>().Where(r => Query.NotExists("_deleted").Inject());
        }

        /*
        protected virtual void InsertCrudEventEntry(CrudEventEntry crudEventEntry)
        {
            _eventLogRepository.CreateResource(crudEventEntry);
        }

        private static TResource GetResourceForEventLog(TResource sourceResource, params Expression<Func<TResource, object>>[] explicitPropertyList)
        {
            var targetResource = Activator.CreateInstance(sourceResource.GetType()) as TResource;
            foreach (var property in explicitPropertyList)
            {
                var propData = property.Compile().Invoke(sourceResource);
                var setter = ExpressionHelpers.GetPropertyInfo(property);
                setter.SetValue(targetResource, propData, null);
            }
            return targetResource;
        }
        */

        public string CreateResource(TResource resource)
        {
            PrepareNewResource(resource);
            ResourceCollection.Insert(resource);
            /*
            var crudEventEntry = new CrudEventEntry
            {
                Operation = CrudOperation.Create,
                ResourceId = resource.Id,
                New = resource,
                Old = null
            };
            InsertCrudEventEntry(crudEventEntry);
            */
            return resource.Id;
        }

        protected virtual Expression<Func<TResource, object>>[] PrepareExplicitPropertyList(params Expression<Func<TResource, object>>[] explicitPropertyList)
        {
            if (explicitPropertyList.Length == 0)
            {
                explicitPropertyList = AllPropertiesToUpdate;
            }
            return explicitPropertyList;
        }

        protected virtual void PrepareNewResource(TResource resource)
        {
            resource.Id = ObjectId.GenerateNewId().ToString();
        }

        protected virtual void PrepareUpdateResource(TResource resource)
        {
        }

        public void UpdateResource(TResource resource, params Expression<Func<TResource, object>>[] explicitPropertyList)
        {
            PrepareUpdateResource(resource);
            explicitPropertyList = PrepareExplicitPropertyList(explicitPropertyList);

            //var modified = GetResourceForEventLog(resource, explicitPropertyList);

            var current = GetResource(resource.Id, explicitPropertyList);

            var query = Query<TResource>.EQ(r => r.Id, resource.Id);
            var update = CreateUpdateBuilderForProperties(resource, explicitPropertyList);
            var writeConcernResult = ResourceCollection.Update(query, update);

            if (writeConcernResult.DocumentsAffected == 0)
            {
                throw new ResourceNotFoundException();
            }

            /*
            var crudEventEntry = new CrudEventEntry
            {
                Operation = CrudOperation.Update,
                ResourceId = resource.Id,
                Old = current,
                New = modified
            };
            InsertCrudEventEntry(crudEventEntry);
            */

        }

        protected virtual UpdateBuilder<TResource> CreateUpdateBuilderForProperties(TResource resource,
            params Expression<Func<TResource, object>>[] explicitPropertyList)
        {
            var update = new UpdateBuilder<TResource>();

            foreach (var property in explicitPropertyList)
            {
                var propData = property.Compile().Invoke(resource);
                update.Set(property, propData);
            }
            return update;
        }

        public void Delete(string id)
        {
            var writeConcernResult = ResourceCollection.Update(Query<TResource>.EQ(r => r.Id, id), Update.Set("_deleted", BsonType.Null));
            if (writeConcernResult.DocumentsAffected > 0)
            {
                /*
                var crudEventEntry = new CrudEventEntry
                {
                    Operation = CrudOperation.Delete,
                    ResourceId = id,
                    ResourceType = typeof(TResource).Name,
                    Old = null,
                    New = null
                };
                InsertCrudEventEntry(crudEventEntry);
                */
            }
            ResourceCollection.Update(Query<TResource>.EQ(r => r.Id, id), Update.Set("_deleted", BsonType.Null));
        }

        protected static readonly Expression<Func<TResource, object>>[] AllPropertiesToUpdate = GetAllProperties();

        private static Expression<Func<TResource, object>>[] GetAllProperties()
        {
            var properties = typeof(TResource)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetSetMethod() != null); //has public setter

            var typeExpression = Expression.Parameter(typeof(TResource));
            var ee = new List<Expression<Func<TResource, object>>>();
            foreach (var propertyInfo in properties)
            {
                var me = Expression.Property(typeExpression, propertyInfo) as Expression;
                if (propertyInfo.PropertyType.IsValueType)
                {
                    me = Expression.Convert(me, typeof(object));
                }

                var lambda = Expression.Lambda<Func<TResource, object>>(me, typeExpression);
                ee.Add(lambda);
            }
            var expressions = from property in properties
                              let memberExpression = GetMemberExpression(typeExpression, property)
                              select Expression.Lambda<Func<TResource, object>>(memberExpression, typeExpression);

            return expressions.ToArray();
        }

        private static Expression GetMemberExpression(Expression typeExpression, PropertyInfo property)
        {
            if (!property.PropertyType.IsValueType)
            {
                return Expression.Property(typeExpression, property);
            }

            var propertyExpression = Expression.Property(typeExpression, property);
            return Expression.Convert(propertyExpression, typeof(object));
        }
    }
}