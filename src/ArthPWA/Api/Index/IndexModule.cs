using Nancy;

namespace ArthPWA.Api.Index
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = _ => "Hello Frickin World!";
        }
    }
}