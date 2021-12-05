using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Musicalog.Core.Services;
using Musicalog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musicalog.Core.Enums;
using Musicalog.Infra.Repositories;
using Musicalog.Core.Interfaces;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;

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

            //var albumList = new List<Album>();
            //albumList.Add(new Album { Id = 1, ArtistName = "Artist1", Title = "Title1", Stock = 1, Type = AlbumType.CD });
            //services.AddSingleton(typeof(IAlbumService), new AlbumService(new InMemoryMusicalogRepository<Album>(albumList)));
            
            SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("MusicalogDB"));
            services.AddSingleton(typeof(IAlbumService), new AlbumService(new DapperMusicalogRepository<Album>(connection)));
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
