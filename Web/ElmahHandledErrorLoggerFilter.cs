using Elmah;
using System.Web.Http.Filters;

public class ElmahHandledErrorLoggerFilter : ExceptionFilterAttribute
{
    public override void OnException(HttpActionExecutedContext actionExecutedContext)
    {
        base.OnException(actionExecutedContext);

        ErrorSignal.FromCurrentContext().Raise(actionExecutedContext.Exception);
    }
}