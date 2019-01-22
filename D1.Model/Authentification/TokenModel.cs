using System;
using System.Collections.Generic;
using System.Text;

namespace D1.Model
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
