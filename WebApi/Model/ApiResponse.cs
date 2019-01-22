

using System;
using D1.Model.Extentions;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Identity;

namespace WebApi.Model
{
    public class ApiResponse
    {
        public int Status { get; set; }

        public string Description { get; set; }
        public long Timestamp { get; set; }

        public ApiResponse()
        {
            Status = 1;
            Description = "Success";
            Timestamp = DateTime.UtcNow.CovertToTimestamp();
        }
    }

    public class ApiResponse<T>:ApiResponse where T : class
    {
        private readonly T model;

        public T Model { get; set; }

        public ApiResponse(T model)
        {
            this.model = model;
        }

    }
}
