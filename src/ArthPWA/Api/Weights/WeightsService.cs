using System;

namespace ArthPWA.Api.Weights
{
    public interface IWeightsService
    {
        string Create();
    }

    public class WeightsService : IWeightsService
    {
        public WeightsService()
        {

        }

        public string Create()
        {
            throw new NotImplementedException();
        }
    }
}