using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace lab1_api.Models
{
    public class GenericApiResponse<T>
    {
        [SwaggerSchema("Response code indicating the result of the API call")]
        public int ResponseCode { get; set; } = 101000; // Default code

        [SwaggerSchema("Data or payload returned by the API")]
        public T? Data { get; set; }

        [SwaggerSchema("Message providing additional information about the response")]
        public string? Message { get; set; }

        [JsonIgnore] // Ignored in serialization
        public bool IsSuccess => IsSuccessCode(ResponseCode);

        private bool IsSuccessCode(int code)
        {
            int mainCode = code % 1000;
            return mainCode >= 900;
        }

        public static GenericApiResponse<T> Success(T data)
        {
            return new GenericApiResponse<T>
            {
                ResponseCode = 200,
                Data = data,
                Message = "Successfully"
            };
        }

        public static GenericApiResponse<T> Error(string errorMessage)
        {
            return new GenericApiResponse<T>
            {
                ResponseCode = 101001,
                Data = default,
                Message = errorMessage
            };
        }
    }
}
