using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Musicalog.Core.Services;
using Musicalog.Core.Entities;
using Musicalog.Infra.Repositories;
using Musicalog.Core.Interfaces;
using System.Data.SqlClient;
using System.Data;
using AutoMapper;
using WebApi.Models;
using Musicalog.Core.Enums;
using System;

namespace WebApi
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
            services.AddControllers();
            services.AddSwaggerGen();

            var autoMap = new MapperConfiguration(config => {
                config.CreateMap<Album, AlbumModel>(); 
                config.CreateMap<AlbumModel, Album>().ForMember(d => d.Type, m => m.MapFrom(s => Enum.Parse<AlbumType>(s.Type))); 
            });

            services.AddSingleton(autoMap.CreateMapper());

            services.AddTransient<IDbConnection>(db => new SqlConnection(Configuration.GetConnectionString("MusicalogDB")));
            services.AddSingleton<IMusicalogRepository<Album>, DapperMusicalogRepository<Album>>();
            services.AddSingleton<IAlbumService, AlbumService>();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Musicalog Api v1");
            });
        }
    }
}
