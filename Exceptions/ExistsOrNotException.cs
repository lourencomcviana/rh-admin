using System;

namespace rh_admin.Exceptions
{
    public class ExistsOrNotException : Exception
    {
        public ExistsOrNotException()
        {
        }

        public ExistsOrNotException(string? message) : base(message)
        {
        }
    }
}