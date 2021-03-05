using LightInject;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StateEngine4net.IoC
{
    public interface IStartup
    {
        IConfiguration Configuration { get; }
  //      IWebHostEnvironment Environment { get; }
        void ConfigureServices(IServiceCollection services);
        void ConfigureContainer(IServiceContainer container);
        void Configure(IApplicationBuilder app);

    }

    public abstract class StateEngineStartupBase : IStateEngineStartup
    {
        public IConfiguration Configuration { get; private set; }
//        public IWebHostEnvironment Environment { get; private set; }

        public virtual void ConfigureServices(IServiceCollection services)
        {

        }

        public virtual void ConfigureContainer(IServiceContainer container)
        {

        }

        public virtual void Configure(IApplicationBuilder app)
        {

        }

        public void Init(IConfiguration configuration)
//        public void Init(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
//            Environment = environment;
        }
    }

    public interface IStateEngineStartup : IStartup
    {
        void Init(IConfiguration config);
    }

}
