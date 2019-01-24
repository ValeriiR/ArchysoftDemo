using System;
using D1.Model.Extentions;


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

        public ApiResponse(int status, string description)
        {
            this.Status = status;
            this.Description = description;
        }
    }

    public class ApiResponse<T>:ApiResponse where T : class
    {
        private readonly T _model;

        public T Model { get; set; }

        public ApiResponse(T model)
        {
            this._model = model;
        }

    }
}
