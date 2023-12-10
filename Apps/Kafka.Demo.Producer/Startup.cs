using Kafka.Demo.Producer.Services;

namespace Kafka.Demo.Producer
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services) =>
            services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddTransient<IProducerService, ProducerService>()
                .AddControllers();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(x => x.MapControllers());
        }
    }
}

