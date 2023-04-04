using System;

namespace OMCC.Plugins.GameManager
{
    public class ValidationException : Exception
    {
        public ValidationException(string msg, bool launchable=false) : base(msg)
        {
            Launchable = launchable;
        }
        public bool Launchable { get; set; }
    }
}
