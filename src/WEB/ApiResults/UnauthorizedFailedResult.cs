using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WEB.ViewModels
{
    public class UnauthorizedFailedResult: ObjectResult
    {
        public UnauthorizedFailedResult(ModelStateDictionary modelState):base(new ApiBadRequestResponse(modelState, StatusCodes.Status401Unauthorized))
        {
            StatusCode = StatusCodes.Status401Unauthorized;
        }
        
    }
}