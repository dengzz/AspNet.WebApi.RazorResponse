
namespace System.Web.Http.Controllers
{
    public static class HttpActionContextExtension 
    {
        public static string GetViewPath(this HttpActionContext actionContext)
        {
            return string.Format("~/Views/{0}/{1}.cshtml"
                , actionContext.ControllerContext.ControllerDescriptor.ControllerName
                , actionContext.ActionDescriptor.ActionName);
        }
    }
}
