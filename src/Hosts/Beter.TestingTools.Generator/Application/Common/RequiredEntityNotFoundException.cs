using System.Diagnostics.CodeAnalysis;

namespace Beter.TestingTools.Generator.Application.Common
{
    [ExcludeFromCodeCoverage]
    public class RequiredEntityNotFoundException : Exception
    {
        public RequiredEntityNotFoundException()
        {
        }

        public RequiredEntityNotFoundException(string message) : base(message)
        {
        }

        public RequiredEntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
