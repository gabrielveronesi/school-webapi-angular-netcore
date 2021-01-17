using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartSchool.API.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;

namespace SmartSchool.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Define as rotas dos controller??.
        public void ConfigureServices(IServiceCollection services)
        {
            //Dado a rota de Aluno(AlunoController) por exemplo, eu posso enjetar o SmartContext dentro da controller!
            services.AddDbContext<SmartContext>(
                context => context.UseSqlite(Configuration.GetConnectionString("Default"))
            
             );
            
            //injeção de dependencia?
            //services.AddSingleton<Repository, Repository>();
            //services.AddTransient<Repository, Repository>();
            services.AddScoped<IRepository, Repository>();
            
            services.AddControllers()
                    .AddNewtonsoftJson(
                        options => options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //Faz isso pro automapper procurarm dentro do dos assemblis (dlls) quem está herdando dos profiles

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            })

            .AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true; // Não definiu a versao? a versão padrão vai ser a linha de baixo
                options.DefaultApiVersion = new ApiVersion(1,0);
                options.ReportApiVersions = true;
            });

            var apiProviderDescription = services.BuildServiceProvider()
                                                 .GetService<IApiVersionDescriptionProvider>();

            //add Swagger
            services.AddSwaggerGen(options => {
            
                //VERSIONANDO O DOCUMENTO SWAGGER - cada uma das versões vai ter o mesmo versionamento.
                foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                {  
            
                options.SwaggerDoc(
                    description.GroupName,
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "SmartSchool API",
                    Version = description.ApiVersion.ToString(),
                    TermsOfService = new Uri("http://SeusTermosDeUso.com"),
                                Description = "BORA PORRA! CRIANDO UMA API",
                                License = new Microsoft.OpenApi.Models.OpenApiLicense
                                {
                                    Name = "SmartSchool License",
                                    Url = new Uri("https://www.instagram.com/ggabrielveronesi/")
                                },
                                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                                {
                                    Name = "Gabriel Veronesi",
                                    Email = "veronesigabriel@live.com",
                                    Url = new Uri("https://www.facebook.com/gabriel.veronesi")
                                }
                                
                });
            } //foreach
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                options.IncludeXmlComments(xmlCommentsFullPath);
        });

        }


        // 
        public void Configure(IApplicationBuilder app, 
                              IWebHostEnvironment env,
                              IApiVersionDescriptionProvider apiProviderDescription)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseSwagger()
                .UseSwaggerUI(options => 
                {
                    foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());    
                    }
                    
                    options.RoutePrefix ="";

                });

                

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
