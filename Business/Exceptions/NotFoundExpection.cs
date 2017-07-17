using System;

namespace Business.Exceptions
{
    public class NotFoundExpection : Exception
    {
        public NotFoundExpection(string message) : base(message)
        { }
    }
}