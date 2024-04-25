using System.Diagnostics.CodeAnalysis;

namespace Beter.TestingTools.Generator.Application.Common
{
    [ExcludeFromCodeCoverage]
    public class DomainException : Exception
    {
        public DomainException()
        {
        }

        public DomainException(string message) : base(message)
        {
        }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
