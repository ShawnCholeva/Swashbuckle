using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;

namespace SwashbuckleExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info
				{
					Description = "Swashbuckle Test API"
				});
			});

			services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
			app.UseStaticFiles();

			if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.IndexStream = () => GetType().GetTypeInfo().Assembly
					.GetManifestResourceStream("SwashbuckleExample.Swagger.index.html");
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swashbuckle Test API v1");
				c.RoutePrefix = string.Empty;
			});

			app.UseMvc();
        }
    }
}
