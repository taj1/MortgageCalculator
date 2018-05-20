using log4net;
using log4net.Config;
using MortgageCalculator.Contracts.Services;
using System.Web.Http;
using Unity;

namespace MortgageCalculator.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(WebApiApplication));

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //Configure logging
            XmlConfigurator.Configure();

            log.Info("Application starting...");

            //Initialize the cache
            var mortgageService = UnityConfig.Container.Resolve<IMortgageService>();
            mortgageService.GetAllMortgages();

            log.Info("Cache Initialized...");
        }
    }
}
