using System;

namespace OMCCore.Core
{
    public class RegistryException : Exception
    {
        public RegistryException(string  message) : base(message) { }
        public static RegistryException REGISTRY_CLOSED { get; } = new RegistryException("Registry have closed");
    }
}
