using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NotificationService.Application.Settings;
using NotificationService.Infrastructure;
using NotificationService.RabbitQueue;
using NotificationService.Workers;

namespace NotificationService.Web
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
            services.AddInfrastructure();
            
            services.Configure<TelegramSettings>(Configuration.GetSection("TelegramSettings"));
            services.Configure<SmscSettings>(Configuration.GetSection("SmscSettings"));
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            
            services.AddSingleton(sp => RabbitHutch.CreateBus(
                    Configuration.GetSection("RabbitMQSettings").Get<RabbitMQSettings>()));
            
            services.AddHostedService<QueueWorker>();

            services.AddHttpClient();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NotificationService.Web", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
            }

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NotificationService.Web v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}