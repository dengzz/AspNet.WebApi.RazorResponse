using RazorEngine;
using System.Net;
using System.Net.Http;
using RazorEngine.Templating;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection;
using Zmmn.Infrastructure.View;
using System.Web.Http.Controllers;

namespace System.Web.Http
{
    public static class ApiControllerExtension
    {
        public static HttpResponseMessage View(this ApiController apiController)
        {
            var viewPath = apiController.ActionContext.GetViewPath();
            return RenderView(viewPath);
        }

        private static HttpResponseMessage RenderView(string name)
        {
            IHtmlRender htmlRender;
            htmlRender = new RazorHtmlRender();
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            httpResponseMessage.Content = htmlRender.Render(name);
            MediaTypeHeaderValue mediaTypeHeaderValue = new MediaTypeHeaderValue("text/html");
            mediaTypeHeaderValue.CharSet = System.Text.Encoding.UTF8.WebName;
            httpResponseMessage.Content.Headers.ContentType = mediaTypeHeaderValue;
            return httpResponseMessage;
        }
    }
}
