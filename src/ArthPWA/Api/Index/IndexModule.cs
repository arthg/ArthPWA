using Nancy;

namespace ArthPWA.Api.Index
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = _ => Negotiate.WithView("Web/index.html");
        }
    }
}