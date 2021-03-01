using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using rh_admin.Models;
using rh_admin.Repositorys;
using rh_admin.Services;

namespace rh_admin
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Aplicação de exemplo para gestão de funcionários",
                        Version = "v1",
                        Description = "Exemplo de API REST criada para gerir funcionários",
                        Contact = new OpenApiContact
                        {
                            Name = "Lourenço Viana",
                            Email = "lourenco.m.c.viana@gmail.com",
                            Url = new Uri("https://github.com/lourencomcviana/rh-admin")
                        }
                    });
            });

            services.AddDbContext<FuncionarioContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("db")));

            services.AddScoped<IGenericRepository, GenericGenericRepository<FuncionarioContext>>();

            services.AddTransient<IRepository<Funcionario, string>, Repository<Funcionario, string>>();

            services.AddTransient<IFuncionarioRepository, FuncionarioRepository>();
            services.AddTransient<ITelefoneRepository, TelefoneRepository>();
            services.AddTransient<FuncionarioService>();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "gestão de funcionários v1"); });


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}