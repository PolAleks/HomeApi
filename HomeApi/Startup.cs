using HomeApi.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeApi
{
    public class Startup
    {
        /// <summary>
        /// �������� ������������ �� ����� Json
        /// </summary>
        private IConfiguration Configuration { get; } = 
            new ConfigurationBuilder()
            .AddJsonFile("HomeOptions.json")
            .Build();

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // ��������� ����� ������
            services.Configure<HomeOptions>(Configuration);

            // ��� �� ����� �������������, �� � MVC �� ����� ������ AddControllersWithViews()
            services.AddControllers();

            // ������������ �������������� ��������� ������������ WebApi � �������������� Swagger
            services.AddSwaggerGen(c => { 
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "HomeApi", 
                    Version = "v1" }); 
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ����������� ����������� ��� ������� ��� ���������� ��������
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HomeApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // ������������ �������� � �������������
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
