using Microsoft.AspNetCore.Mvc.Filters;
using VPMS;

namespace VPMSWeb.Middleware
{
	public class ErrorHandlingFilter : ExceptionFilterAttribute
	{
		public override void OnException(ExceptionContext context)
		{
			var exception = context.Exception;

			Program.logger.Error("Unhandled Error >> ", exception);

			context.ExceptionHandled = true; //optional 
		}
	}
}
