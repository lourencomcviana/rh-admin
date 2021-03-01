namespace rh_admin.Exceptions
{
    public class ExistsOrNotException : System.Exception
    {
        public ExistsOrNotException()
        {
        }

        public ExistsOrNotException(string? message) : base(message)
        {
        }
    }
}