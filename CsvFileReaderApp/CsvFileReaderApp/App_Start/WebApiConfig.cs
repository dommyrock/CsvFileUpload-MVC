using CsvFileReaderApp.Models;

using Microsoft.AspNet.OData.Extensions;

using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;

namespace CsvFileReaderApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ////Web API configuration and services
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(
                new RequestHeaderMapping("Accept", "text/html", StringComparison.InvariantCultureIgnoreCase, true, "application/json"));
            GlobalConfiguration.Configuration.EnableDependencyInjection();
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();
            //config.MapHttpAttributeRoutes(new CentralizedPrefixProvider("odata"));//custom class for prefixing "odata"
            //////config.Filters.Add(new ExceptionHandlingAttribute());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //This config BELLOW  is enough alone for AUTO-generated odata controller "PodacisController"
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Podaci>("Podacis");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}