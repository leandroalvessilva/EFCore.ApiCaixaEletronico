using ApiCaixaEletronico.DAO;
using ApiCaixaEletronico.DAO.DAO;
using ApiCaixaEletronico.DAO.Interface;
using ApiCaixaEletronico.Service.Interface;
using ApiCaixaEletronico.Service.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApiCaixaEletronico
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsetting.json", optional: true, reloadOnChange: true);

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    );
            });

            services.AddDbContext<CommonDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Defaultconnection"));
            });

            services.AddTransient<IOperacoesBancariasDAO, OperacoesBancariasDAO>();
            services.AddTransient<IOperacoesBancariasService, OperacoesBancariasService>();
            services.AddTransient<ICaixaEletronicoDAO, CaixaEletronicoDAO>();
            services.AddTransient<ICaixaEletronicoService, CaixaEletronicoService>();

            services.AddControllers().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AllowSpecificOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
