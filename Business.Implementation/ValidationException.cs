using System;
using System.Runtime.Serialization;

namespace Business.Implementation
{
    [Serializable]
    public class ValidationException : Exception
    {
        private const string DefaultMessage = "Validation exception has occured.";
        
        public ValidationException() : base(DefaultMessage)
        {
        }

        public ValidationException(string message) 
            : base(message)
        {
        }

        public ValidationException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected ValidationException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}