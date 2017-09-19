using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WEB.ViewModels
{
    public class ApiBadRequestResponse : ApiResponse
    {
        public IEnumerable<ErrorItem> Errors { get; }

        public ApiBadRequestResponse(ModelStateDictionary modelState, Int32 statusCodes = StatusCodes.Status400BadRequest)
            : base(statusCodes)
        {
            if (modelState.IsValid)
            {
                throw new ArgumentException("ModelState must be invalid", nameof(modelState));
            }

            Errors = modelState.Keys.SelectMany(key => modelState[key].Errors.Select(x => new ErrorItem
            {
                Field = key,
                Message = x.ErrorMessage
            }));
        }
    }
}