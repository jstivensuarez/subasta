using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Subasta.core;
using Subasta.core.interfaces;
using Subasta.core.services;
using Subasta.repository;
using Subasta.repository.interfaces;
using Subasta.repository.repositorys;

namespace Subasta
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
            services.AddDbContext<SubastaContext>(options =>
               options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Subasta")));

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperManager());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<IAnimalService, AnimalService>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IClienteService, ClienteService>();

            services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
            services.AddScoped<IDepartamentoService, DepartamentoService>();

            services.AddScoped<IEventoRepository, EventoRepository>();
            services.AddScoped<IEventoService, EventoService>();

            services.AddScoped<ILoteRepository, LoteRepository>();
            services.AddScoped<ILoteService, LoteService>();

            services.AddScoped<IMunicipioRepository, MunicipioRepository>();
            services.AddScoped<IMunicipioService, MunicipioService>();

            services.AddScoped<ISubastaRepository, SubastaRepository>();
            services.AddScoped<ISubastaService, SubastaService>();

            services.AddScoped<ITipoDocumentoRepository, TipoDocumentoRepository>();
            services.AddScoped<ITipoDocumentoService, TipoDocumentoService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
