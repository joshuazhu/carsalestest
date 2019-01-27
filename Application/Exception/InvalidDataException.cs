namespace Application.Exception
{
    public class InvalidDataException : System.Exception
    {
        public InvalidDataException()
        {

        }

        public InvalidDataException(string message) : base(message)
        {

        }
    }
}
