using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using item_workflow.Database;
using Microsoft.EntityFrameworkCore;
using item_workflow.Model;
using item_workflow.Workflows;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Persistence.SqlServer;
using Serilog;

namespace item_workflow
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Information("[Startup] (CTOR)");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<TestDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TestDB")));
            services.AddControllers();
       
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddWorkflow(x => x.UseSqlServer(Configuration.GetConnectionString("WorkflowDB"), true, true));
            //services.AddWorkflow();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var host = app.ApplicationServices.GetService<IWorkflowHost>();
            host.RegisterWorkflow<ItemWorkflow, Item>();
            host.Start();

            Log.Information("[Workflow Registered] (ItemWorkflow)");

            //var initialData = new Item();
            //var workflowId = host.StartWorkflow("ItemWorkflow", 1, initialData).Result;


        }
    }
}
