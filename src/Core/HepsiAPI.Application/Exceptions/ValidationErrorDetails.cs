using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HepsiAPI.Application.Exceptions
{
    public class ValidationErrorDetails : ProblemDetails
    {
        public object Errors { get; set; }
    }
}
