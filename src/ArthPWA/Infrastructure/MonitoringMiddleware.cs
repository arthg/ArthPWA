using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using AppFunc = System.Func<
        System.Collections.Generic.IDictionary<string, object>, 
        System.Threading.Tasks.Task>; 

namespace ArthPWA.Infrastructure
{
    public sealed class MonitoringMiddleware
    {
        private AppFunc _next;
        private Func<Task<bool>> _healthCheck;

        public MonitoringMiddleware(AppFunc next, Func<Task<bool>> healthCheck)
        {
            _next = next;
            _healthCheck = healthCheck;
        }

        /*
        public Task Invoke(IDictionary<string, object> env)
        {
            var context = new OwinContext(env);
            if (context.Request.Path.StartsWithSegments(monitorPath))
                return HandleMonitorEndpoint(context);
            else
                return this.next(env);
        }
        */
    }
}