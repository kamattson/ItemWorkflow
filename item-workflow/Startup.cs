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
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using item_workflow.Middleware;
using item_workflow.Steps;

namespace item_workflow
{
    public class Startup
    {

        private string _connection = null;
        private string _wfconnection = null;

        public Startup(IConfiguration configuration)
        {
            Log.Information("[Startup] (CTOR)");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var builder = new SqlConnectionStringBuilder(
                Configuration.GetConnectionString("TestDB"));
                builder.Password = Configuration["WFPass"];
            _connection = builder.ConnectionString;

            var wfcbulder = new SqlConnectionStringBuilder(
                Configuration.GetConnectionString("WorkflowDB"));
                wfcbulder.Password = Configuration["WFPass"];
            _wfconnection = wfcbulder.ConnectionString;

            services.AddDbContext<ItemDbContext>(options => options.UseSqlServer(_connection));
            services.AddControllers();
       
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddWorkflow(x => x.UseSqlServer(_wfconnection, true, true));
            //services.AddWorkflow();

            services.AddWorkflowStepMiddleware<LogCorrelationStepMiddleware>();

            // Add some pre workflow middleware
            // This middleware will run before the workflow starts
            services.AddWorkflowMiddleware<AddDescriptionWorkflowMiddleware>();

            // Add some post workflow middleware
            // This middleware will run after the workflow completes
            services.AddWorkflowMiddleware<PrintWorkflowSummaryMiddleware>();

            services.AddTransient<NewItem>();
            services.AddTransient<ProcessApproval>();
            services.AddTransient<PricingAutoAssign>();

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

            string configkey = Configuration["ItemWF:ServiceApiKey"];

            Log.Information("[Workflow Registered] (ItemWorkflow) {configkey}", configkey);
            //var initialData = new Item();
            //var workflowId = host.StartWorkflow("ItemWorkflow", 1, initialData).Result;

        }
    }
}
