using System;
using System.Linq;
using System.Linq.Expressions;
using ArthPWA.Api.Weights;
using ArthPWA.Models;

namespace ArthPWA.MongoDB
{
    public class WeightsRepository : IWeightsRepository
    {
        public string CreateResource(WeightEntry resource)
        {
            return "Hello";
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public WeightEntry GetResource(string id, params Expression<Func<WeightEntry, object>>[] explicitPropertyList)
        {
            throw new NotImplementedException();
        }

        public IQueryable<WeightEntry> GetResources()
        {
            throw new NotImplementedException();
        }

        public void UpdateResource(WeightEntry resource, params Expression<Func<WeightEntry, object>>[] explicitPropertyList)
        {
            throw new NotImplementedException();
        }
    }
}