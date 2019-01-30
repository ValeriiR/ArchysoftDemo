using System;
using System.Linq;
using D1.Model.Extentions;
using Microsoft.AspNetCore.Mvc.ModelBinding;


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

        public ApiResponse(ModelStateDictionary state)
        {
            if (state == null) return;

            Status = -1;
            Timestamp = DateTime.Now.CovertToTimestamp();

            var validationKeys = state.Keys.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            foreach (var validationKey in validationKeys)
            {
                var property = state[validationKey];
                if (property.ValidationState == ModelValidationState.Invalid)
                {
                    Description = property.Errors.FirstOrDefault()?.ErrorMessage;
                }
            }
        }
    }

    public class ApiResponse<T>:ApiResponse where T : class
    {
      //  private readonly T _model;

        public T Model { get; set; }

        public ApiResponse()
        {

        }
        public ApiResponse(T model)
        {
           // this._model = model;
           Model = model;
        }

    }
}
