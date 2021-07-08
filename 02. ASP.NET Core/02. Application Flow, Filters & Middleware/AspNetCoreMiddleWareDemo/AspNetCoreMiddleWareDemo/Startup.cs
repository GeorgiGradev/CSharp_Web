using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMiddleWareDemo
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.Map("/softuni", app =>
            {
                app.UseWelcomePage(); 
            });

            app.Run(async (request) =>
            {
                await request.Response.WriteAsync("I am the one and only!");
            });

            app.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("1");
                    if (DateTime.Now.Second % 2 == 0)
                    {
                        await next();
                    }
                    await context.Response.WriteAsync("6");
                });
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("2");
                await next();
                await context.Response.WriteAsync("5");
            });
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("2");
                await context.Response.WriteAsync("4");
            });
        }
    }
}
