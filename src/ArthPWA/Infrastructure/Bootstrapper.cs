using Nancy;
using ArthPWA.MongoDB;
using Nancy.TinyIoc;
using System.Configuration;

namespace ArthPWA.Infrastructure
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private static readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString;

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<IMongoDb>((c, p) => new ArthNetDatabase(CONNECTION_STRING));
        }
    }
}