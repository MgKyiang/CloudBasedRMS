using CloudBasedRMS.View.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CloudBasedRMS.View.HttpModules
{
    /// <summary>
    /// Module to append lang parameter to the requested url if it's absent or unsupported
    /// </summary>
    public class LangQueryAppenderModule : IHttpModule
    {
        /// <summary>
        /// List of supported locales
        /// </summary>
        private readonly IList<string> _supportedLocales;

        /// <summary>
        /// We need to have controllers list to correctly handle situations
        /// when target method name is missed
        /// </summary>
        private readonly IList<string> _controllersNamesList;

        public LangQueryAppenderModule()
        {
            // Get list of supported locales 
            _supportedLocales = Utils.LocalizationHelper.GetSupportedLocales();

            // Get controllers list of current project by reflection
            var asmPath = HttpContext.Current.Server.MapPath("~/bin/CloudBasedRMS.View.dll");
            Assembly asm = Assembly.LoadFile(asmPath);

            var controllerTypes = asm.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type));
            _controllersNamesList = new List<string>();

            foreach (var controllerType in controllerTypes)
            {
                var fullName = controllerType.Name;

                // We need only name part of Controller class that is used in route
                _controllersNamesList.Add(fullName.Substring(0, fullName.Length - "Controller".Length));
            }
        }

        // In the Init function, register for HttpApplication 
        // events by adding your handlers.
        public void Init(HttpApplication application)
        {
            application.BeginRequest += (new EventHandler(this.Application_BeginRequest));
        }

        private void Application_BeginRequest(Object source, EventArgs e)
        {
            try
            {
                HttpApplication app = (HttpApplication)source;
                HttpContext ctx = app.Context;

                // We will redirect to url with defined locale only in case for HTTP GET verb
                // cause we assume that all requests with other verbs will be called from site directly
                // where all the urls created with URLHelper, so it complies with routing rules and will contain "lang" parameter
                if (string.Equals(ctx.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                {
                    var localisedUri = LocalizationHelper.GetLocalisedUrl(ctx.Request.Url, _controllersNamesList, ctx.Request.UserLanguages);
                    if (!string.IsNullOrEmpty(localisedUri))
                        // Perform redirect action to changed url if it exists
                        ctx.Response.Redirect(localisedUri);
                }
            }
            catch (Exception)
            {
                // Some logging logic could be here
            }
        }

        public void Dispose() { }
    }
}