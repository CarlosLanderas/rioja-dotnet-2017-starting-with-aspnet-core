using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RiojaDotNet.Abstractions;
using RiojaDotNet.Services;

namespace RiojaDotNet
{
    public class MiddlewareSamples
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILogroñoGuide, LogroñoGuide>();
        }
       
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.Use(async (context, next) =>
            {
                Console.WriteLine("Entering middleware 1");
                context.Response.Headers.Add("Middleware1Header", "value");
                await next.Invoke();
                Console.WriteLine("Exiting middleware 1");
            });

            app.Map("/secretpath", appBuilder =>
            {
                appBuilder.Run(async context => await context.Response.WriteAsync("Entered secret path branch"));
            });

            app.UseMiddleware<HeaderInspectorMiddleware>();

            app.MapWhen(context => context.Request.Query.ContainsKey("branch"), appBuilder =>
            {
                appBuilder.Run(async context =>
                await context.Response.WriteAsync("Entered conditional branch, querystring key was found"));
            });

            app.Run(async context =>
            {
                var logroñoGuide = context.RequestServices.GetService<ILogroñoGuide>();
                await context.Response.WriteAsync(
                    JsonConvert.SerializeObject(logroñoGuide.GetPlacesOfInterest()
                    ));
            });

        }
    }

    public class HeaderInspectorMiddleware
    {
        private RequestDelegate next;
        public HeaderInspectorMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("secret"))
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("The request is invalid");
            }
            else
            {
                await next(context);
            }
        }
    }
}
