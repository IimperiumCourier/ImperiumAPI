using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ImperiumLogistics.SharedKernel.APIWrapper
{
    public class ServiceResponse
    {
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
        [JsonPropertyName("errors")]
        public List<string> Errors { get; set; } = new List<string>();
    }

    public class ServiceResponse<T> : ServiceResponse where T : class
    {
        public T ResponseObject { get; set; }
        public bool ResponseObjectExists => ResponseObject != null;

        public static ServiceResponse<T> Success(T instance, string message = "Successful") => new ServiceResponse<T>()
        {
            Message = message,
            IsSuccessful = true,
            ResponseObject = instance
        };

        public static ServiceResponse<T> Error(string error, string message = "") => new ServiceResponse<T>()
        {
            IsSuccessful = false,
            Errors = new List<string> { error },
            Message = string.IsNullOrEmpty(message) ? error : message
        };

        public static ServiceResponse<T> NotFound(T instance = null, string message = "") => new ServiceResponse<T>()
        {
            Message = message,
            IsSuccessful = false,
            ResponseObject = instance,
            Errors = new List<string> { message }
        };
    }
}
