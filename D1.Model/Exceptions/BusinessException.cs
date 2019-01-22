

using System;

namespace D1.Model.Exceptions
{
    class BusinessException:Exception
    {
        public BusinessException(string message):base(message)
        {

        }
    }
}
