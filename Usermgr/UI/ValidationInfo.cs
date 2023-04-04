using OMCCore.Globalization;

namespace OMCC.Plugins.UserManager.UI
{
    public class ValidationInfo
    {
        public ValidationInfo(bool isValid, Text? invalidMessage = null)
        {
            IsValid = isValid;
            InvalidMessage = invalidMessage;
        }

        public bool IsValid { get; set; }
        public Text? InvalidMessage { get; set; }
    }
}
