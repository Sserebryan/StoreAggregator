using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WEB.ViewModels
{
    public class ApiOkResult: ObjectResult
    {
        public ApiOkResult(Object result) : base(new ApiOkResponse(result,
            StatusCodes.Status200OK))
        {
            StatusCode = StatusCodes.Status200OK;
        }
    }
}