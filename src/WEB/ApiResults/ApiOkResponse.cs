using System;
using Microsoft.AspNetCore.Http;

namespace WEB.ViewModels
{
    public class ApiOkResponse : ApiResponse
    {
        public ApiOkResponse(object result, Int32 statusCode)
            :base(statusCode, result)
        {
        }
    }
}