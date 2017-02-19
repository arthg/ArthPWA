using Nancy;

namespace ArthPWA.Api.Weights
{
    public class WeightsModule : NancyModule
    {
        public WeightsModule(IWeightsService weightsService) : base("/weights/")
        {
            Get["/"] = _ => "Hello!";

            Post["/"] = _ =>
            {
                var newId = weightsService.Create();
                var locationUri = ModulePath + newId;
                return Negotiate
                .WithModel(new { id = newId })
                .WithHeader("Location", locationUri)
                .WithStatusCode(HttpStatusCode.Created);
            };
        }
    }
}