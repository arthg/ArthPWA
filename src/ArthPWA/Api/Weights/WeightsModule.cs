using Nancy;

namespace ArthPWA.Api.Weights
{
    public class WeightsModule : NancyModule
    {
        public WeightsModule() : base("/weights")
        {
            Get["/"] = _ => "Hello!";

            Post["/"] = _ => { return Negotiate
                .WithModel(new { id = 0 })
                .WithStatusCode(HttpStatusCode.Created); };
        }
    }
}