using System;

namespace RentalSystem.MailSender.Infrastructure.Configuration.Exceptions
{
    public class AppConfigurationException : Exception
    {

        public AppConfigurationException() { }

        public AppConfigurationException(string message) : base(message)
        {
        }

        public AppConfigurationException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
