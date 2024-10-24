namespace CRUDFramework
{
    class InternalServerException:Exception
    {
        public InternalServerException(string message, Exception innerException)
                    : base(message, innerException)
        {
        }
        public InternalServerException(string message)
                    : base(message)
        {
        }
    }
}
