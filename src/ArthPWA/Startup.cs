using Owin;
using Nancy.Owin;

namespace ArthPWA
{   
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy(options =>
            {
                
            });
        }
    }
}