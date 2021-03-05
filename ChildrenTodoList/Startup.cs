using ChildrenTodoList.Services.CosmosDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ChildrenTodoList
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
            services.Configure<CosmosDBServiceOptions>(Configuration);
            var docClient = new DocumentClient(
                new Uri(Configuration[CosmosDbConfigurationConstants.DbUri]),
                        Configuration[CosmosDbConfigurationConstants.DbKey]);
            services.AddSingleton(docClient);

            services.AddScoped<Services.IChildrenDbService, ChildrenCosmosDbService>();
            services.AddScoped<Services.ITasksDbService, TasksCosmosDbService>();
            services.AddControllers();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "ChildrenTodoList API",
                    Version = "1.0",
                    Description = "API for Children Todo List"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                var envName = env.IsProduction() ? "" : $" ({env.EnvironmentName})";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"Children Todo List{envName}");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
