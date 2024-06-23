using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiAPI.Application.Wrappers
{
    public class BaseResponse
    {
        public BaseResponse(string message, bool success) : this(success)
        {
            Message = message;

        }

        public BaseResponse(bool success)
        {
            Success = success;
        }

        public string? Message { get; }
        public bool Success { get; }
    }

    public class DataResponse<T> : BaseResponse
    {
        public DataResponse(T data, string message, bool success) : base(message, success)
        {
            Data = data;
        }

        public DataResponse(T data, bool success) : base(success)
        {
            Data = data;
        }

        public DataResponse(T data) : base(true)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
