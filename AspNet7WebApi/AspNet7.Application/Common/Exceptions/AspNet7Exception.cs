using System;

namespace AspNet7.Application.Common.Exceptions
{
    public class AspNet7Exception : Exception
    {
        public AspNet7Exception()
        {
        }
        public AspNet7Exception(string businessMessage)
               : base(businessMessage)
        {
        }
    }
}
