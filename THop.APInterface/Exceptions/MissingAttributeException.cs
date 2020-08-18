using System;
using System.Reflection;

namespace THop.APInterface.Exceptions
{
    public class MissingAttributeException : Exception
    {
        public MissingAttributeException(MethodInfo methodInfo, Type attributeType) : base($"Method: {methodInfo.Name} of {methodInfo.DeclaringType} does not contain {attributeType.Name}")
        {
            this.MethodInfo = methodInfo;
            this.AttributeType = attributeType;
        }

        private MethodInfo MethodInfo { get; }
        private Type AttributeType { get; }


    }
}
