using System.Web.Http;

namespace JC.Samples.Web.Api
{
    /// <summary>
    /// Global application entry point.
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Called on application start-up.
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}