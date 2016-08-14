using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BidKaro
{
    using BidKaro.App_Start;

    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// Application_s the start.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        /// <summary>
        /// Handles the EndRequest event of the application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void application_EndRequest(object sender, EventArgs e)
        {
            HttpRequest request = HttpContext.Current.Request;
            HttpResponse response = HttpContext.Current.Response;

            if ((request.HttpMethod == "POST") &&
                (response.StatusCode == 404 && response.SubStatusCode == 13))
            {
                // Clear the response header but do not clear errors and
                // transfer back to requesting page to handle error
                if (HttpContext.Current.Request.Path.ToUpper().Contains("WEBHOOKS"))
                {
                    response.ClearHeaders();
                    response.StatusCode = 200;
                    // response.Write("File length exceeded");
                    HttpContext.Current.Items["error"] = "File exceeded maximum allowed length.";
                    // Response.Redirect(request.AppRelativeCurrentExecutionFilePath);
                }
            }

            // handle Unauthorized ajax requests. it will fire event in changelanguage.ascx in js
            var context = new HttpContextWrapper(Context);
            if (context.Response.StatusCode == 302 && context.Request.IsAjaxRequest())
            {
                context.Response.Clear();
                Context.Response.StatusCode = 401;
            }
        }
    }
}
