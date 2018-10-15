using System;

namespace Gruppo3Esame.Infrastructure
{
    public class Exceptions
    {
        public class NotFoundException : Exception
        {
            public NotFoundException()
            { }

            public NotFoundException(string message) : base(message)
            { }

            public NotFoundException(string message, Exception innerException)
                : base(message, innerException)
            { }
        }
    }
}
