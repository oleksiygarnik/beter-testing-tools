using System.Diagnostics.CodeAnalysis;

namespace Beter.TestingTools.Generator.Application.Common
{
    [ExcludeFromCodeCoverage]
    public class DuplicateEntityException : DomainException
    {
        private const string DefaultMessage = "Entity with given ID already exists.";

        public DuplicateEntityException() : this(DefaultMessage)
        {
        }

        public DuplicateEntityException(string message) : base(message)
        {
        }

        public DuplicateEntityException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
