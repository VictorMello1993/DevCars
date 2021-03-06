using DevCars.API.Filters;
using DevCars.API.Persistence;
using DevCars.Application.Commands.AddCar;
using DevCars.Application.Validators;
using DevCars.Domain.Repositories;
using DevCars.Infrastructure.Persistence.Repositories;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DevCars.API
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
            /*Testando API com bancos de dados em mem?ria. Por isso, ? adequado utilizar singleton para toda a execu??o da API, para n?o perder as 
             * informa??es para qualquer requisi??o que exija acesso a banco de dados*/
            //services.AddSingleton<DevCarsDbContext>();

            var connectionString = Configuration.GetConnectionString("DevCarsConnectionString");

            //Configurando o banco SQL Server
            services.AddDbContext<DevCarsDbContext>(options => options.UseSqlServer(connectionString));

            //Banco de dados em mem?ria
            //services.AddDbContext<DevCarsDbContext>(options => options.UseInMemoryDatabase("DevCars")); 

            //Configura??o de inje??o de depend?ncia do MediatR para o padr?o CQRS
            services.AddMediatR(typeof(AddCarCommand));

            //Configurando a inje??o de depend?ncia do Repository
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICostumerRepository, CostumerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter))) //Configurando filtros de valida??o
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddCarValidator>()); //Adicionando fluent validation                    
                    
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DevCars.API",
                    Version = "v1",

                    //Gerando arquivo xml contendo todos os detalhes da documenta??o do Swagger
                    Contact = new OpenApiContact
                    {
                        Name = "Victor Santos de Mello",
                        Email = "victorsmello93@gmail.com",
                        Url = new Uri("https://github.com/VictorMello1993")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevCars.API v1"));
            }

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
