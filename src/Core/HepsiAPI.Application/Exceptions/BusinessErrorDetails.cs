using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HepsiAPI.Application.Exceptions
{
    public class BusinessErrorDetails : ProblemDetails
    {
        public override string ToString() => JsonConvert.SerializeObject(this);

    }
}
