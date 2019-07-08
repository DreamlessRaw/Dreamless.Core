using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dreamless.Core
{
    public class ApiResult
    {
        public static ApiResult Error(string message)
        {
            return new ApiResult
            {
                IsSuccess = false,
                Message = message,
            };
        }

        public static ApiResult<T> Ok<T>(T data, int count = 0)
        {
            return new ApiResult<T>()
            {
                IsSuccess = true,
                Message = "",
                Data = data,
                Count = count
            };
        }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public int Count { get; set; }
    }
    public class ApiResult<T> : ApiResult
    {
        public new static ApiResult<T> Error(string message)
        {
            return new ApiResult<T>
            {
                IsSuccess = false,
                Message = message,
            };
        }
        public T Data { get; set; }
    }
}
