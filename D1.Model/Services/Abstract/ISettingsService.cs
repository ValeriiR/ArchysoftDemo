using System;
using System.Collections.Generic;
using System.Text;
using D1.Model.Settings;

namespace D1.Model.Services.Abstract
{
    public interface ISettingsService
    {
        JwtSettings JwtSettings { get; set; }
    }
}
