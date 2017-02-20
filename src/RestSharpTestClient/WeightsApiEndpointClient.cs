using System.Threading.Tasks;
using RestSharp;
using System.Threading;
using System;
using Polly;

namespace RestSharpTestClient
{
    public class WeightsApiEndpointClient
    {
        private static Policy retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3000, attempt => TimeSpan.FromMilliseconds(100*Math.Pow(2, attempt)));

        public async Task<IRestResponse> Ttt()
        {
            return await retryPolicy.ExecuteAsync(() => GetWeights());
        }

        private static async Task<IRestResponse> GetWeights()
        {
            var client = new RestClient("http://localhost:60743");
            var request = new RestRequest("weights", Method.GET);
            var cancellationTokenSource = new CancellationTokenSource();

            return await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);
        }
    }
}
