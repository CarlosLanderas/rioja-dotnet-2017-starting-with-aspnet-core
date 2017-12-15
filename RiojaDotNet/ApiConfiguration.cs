using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using RiojaDotNet.Abstractions;
using RiojaDotNet.Services;
using RiojaDotNet.Infractructure.Formatters;
using Microsoft.AspNetCore.Mvc.Formatters;
using RiojaDotNet.Api.Cities.Requests;
using RiojaDotNet.Infractructure.Conventions;
using RiojaDotNet.Infractructure.Filters;
using RiojaDotNet.Infractructure.FormatFilters;
using Microsoft.ApplicationInsights.Extensibility;

namespace RiojaDotNet
{
    public class ApiConfiguration
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvcCore()
                .AddJsonFormatters()
                .AddMvcOptions(options =>
                {
                    options.Conventions.Add(new RequiresHeaderApplicationConvention());
                    options.Filters.Add(typeof(ValidModelStateFilter));
                    options.OutputFormatters.Add(new SiteofInterestsOutputFormatter());
                    options.FormatterMappings.SetMediaTypeMappingForFormat("logronosites", "text/logronosites");
                })
                .AddFormatterMappings()
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssemblyContaining<ApiConfiguration>();
                });

            services.AddSingleton<ILogroñoGuide, LogroñoGuide>();
            services.Add(ServiceDescriptor.Singleton<FormatFilter, NonRequiredFormatFilter>());

        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            TelemetryConfiguration.Active.DisableTelemetry = true;

            app.UseMvc();
        }
    }
}
