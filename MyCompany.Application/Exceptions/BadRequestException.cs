namespace MyCompany.Application.Exceptions
{
    public class BadRequestException : Exception
    {

        public BadRequestException(string message)
            : base(message) // Gọi constructor của lớp Exception cơ sở
        {
        }


        public BadRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
