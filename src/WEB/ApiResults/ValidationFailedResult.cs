using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WEB.ViewModels
{
    public class ValidationFailedResult: ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState) : base(
            new ApiBadRequestResponse(modelState, StatusCodes.Status422UnprocessableEntity))
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}