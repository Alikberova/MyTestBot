using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyTestBot.BoredApi;
using MyTestBot.Commands;
using MyTestBot.Keyboard;
using MyTestBot.Translate;
using MyTestBot.Db;
using Microsoft.EntityFrameworkCore;

namespace MyTestBot.Web
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            
            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }
            else
            {
                builder.AddJsonFile("appsettings.json");
            }
            builder.AddEnvironmentVariables();
            IConfigurationRoot configuration = builder.Build();

            Configuration = configuration;
            StaticConfig = configuration;
        }

        public IConfiguration Configuration { get; }
        public static IConfiguration StaticConfig { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddXmlSerializerFormatters().AddNewtonsoftJson();
            services.AddMvc();

            BotConfig botConfig = Configuration.GetSection("BotConfig").Get<BotConfig>();
            services.AddSingleton(typeof(BotConfig), botConfig);

            string connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ActivityContext>(options => options.UseSqlServer(connection));

            services.AddScoped<ActivityService>();
            services.AddScoped<KeyboardService>();
            services.AddScoped<CommandService>();
            services.AddScoped<BotService>();
            services.AddScoped<TranslateService>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Bot.GetBotClientAsync().Wait();
        }
    }
}
