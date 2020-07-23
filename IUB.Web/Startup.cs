using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IUB.Commands;
using IUB.Keyboard;
using IUB.Translating;
using IUB.Db;
using Microsoft.EntityFrameworkCore;

namespace IUB.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; } //check if possible leave only one config
        public static IConfiguration StaticConfig { get; private set; }
        public static DbContextOptions DbOptions { get; private set; }

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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddMvc();

            BotConfig botConfig = Configuration.GetSection("BotConfig").Get<BotConfig>();
            services.AddSingleton(typeof(BotConfig), botConfig);

            services.AddScoped<KeyboardService>();
            services.AddScoped<CommandService>();
            services.AddScoped<BotService>();
            services.AddScoped<TranslatingService>();
            services.AddScoped<Repository>();

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ActivityContext>(options =>
            {
                DbContextOptionsBuilder optionsBuilder = options.UseSqlServer(connection);
                DbOptions = optionsBuilder.Options;
            });
            //services.AddDbContextPool<ActivityContext>(options => options.UseSqlServer(connection));
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
