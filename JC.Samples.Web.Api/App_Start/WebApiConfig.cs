using JC.Samples.Web.Api.Errors;
using JC.Samples.Web.Api.Errors.Handlers;
using JC.Samples.Web.Api.Errors.Loggers;
using Newtonsoft.Json.Serialization;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace JC.Samples.Web.Api
{
    /// <summary>
    /// Web API Configuration.
    /// </summary>
    public static class WebApiConfig
    {
        #region Methods

        /// <summary>
        /// Registers Web API settings.
        /// </summary>
        /// <param name="config">The HTTP Configuration</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API routes.
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Add the Error filters/handlers/loggers.
            config.Filters.Add(new ApiValidationActionFilter());
            config.Services.Replace(typeof(IExceptionHandler), new ApiExceptionHandler());
            config.Services.Replace(typeof(IExceptionLogger) , new ApiExceptionLogger());

            // Remove support for XML response formatting (JSON only).
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            // Return JSON responses which are formatted as follows.
            // - With indentation for human-readability;
            // - In camelCase to assist JavaScript consumers;
            // - Enums converted to strings.
            var json                                 = config.Formatters.JsonFormatter;
            json.SerializerSettings.Formatting       = Newtonsoft.Json.Formatting.Indented;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            json.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        }

        #endregion
    }
}