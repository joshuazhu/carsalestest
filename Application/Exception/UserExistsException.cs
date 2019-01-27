namespace Application.Exception
{
    public class UserExistsException : System.Exception
    {
        public UserExistsException()
        {

        }

        public UserExistsException(string message) : base(message)
        {

        }
    }
}
