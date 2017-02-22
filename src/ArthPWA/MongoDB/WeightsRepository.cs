using ArthPWA.DataAccess;
using ArthPWA.Models;
using ArthPWA.Api.Weights;

namespace ArthPWA.MongoDB
{
    public class WeightsRepository : RepositoryBase<WeightEntry>, IWeightsRepository
    {
        public WeightsRepository(IMongoDb mongoDatabase)
            : base(mongoDatabase, "weights")
        {
        }
    }
}