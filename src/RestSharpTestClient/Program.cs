using System;

namespace RestSharpTestClient
{
    public class Program
    {
        public static void Main(string[] arg) => new Program().Main();

        private WeightsApiEndpointClient _weightsApiEndpointClient;
        
        public void Main()
        {
            _weightsApiEndpointClient = new WeightsApiEndpointClient();

            var response = _weightsApiEndpointClient.Ttt().Result;

           
            Console.WriteLine(response.Content);
            Console.ReadKey();
        }

    }
}
