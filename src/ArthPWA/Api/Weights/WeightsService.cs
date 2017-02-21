using System;
using ArthPWA.Models;

namespace ArthPWA.Api.Weights
{
    public interface IWeightsService
    {
        string Create();
    }

    public class WeightsService : IWeightsService
    {
        private IWeightsRepository _weightsRepository;

        public WeightsService(IWeightsRepository weightsRepository)
        {
            _weightsRepository = weightsRepository;
        }

        public string Create()
        {
            var weightEntry = new WeightEntry();
            return _weightsRepository.CreateResource(weightEntry);
        }
    }
}