using CommunityToolkit.Mvvm.ComponentModel;
using MaterialDesignThemes.Wpf;
using OMCCore.Globalization;
using System.ComponentModel;
using System.Drawing;

namespace OMCC.Plugins.UserManager.UI
{
    public partial class QuestionModel : ObservableObject
    {
        public event QuestionValidateEventHandler? ValidationInfoChangedEvent;
        public SimpleUserLoginPageViewModel? Parent { get; set; } = null;
        [ObservableProperty] Text title = new Text("");
        [ObservableProperty] string text = "";
        [ObservableProperty] PackIconKind icon = PackIconKind.QuestionMarkBox;
        [ObservableProperty] ValidationInfo validationInfo = new ValidationInfo(true);
        partial void OnTextChanged(string value)
        {
            if (Parent != null)
            {
                this.ValidationInfo = Parent.Validate(this);
            }
        }
        partial void OnValidationInfoChanged(ValidationInfo value)
        { 
            if (ValidationInfoChangedEvent != null)
            {
                ValidationInfoChangedEvent(this, value);
            }
        }
        public QuestionModel(Text title, string text, PackIconKind icon)
        {
            this.Title = title;
            this.Text = text;
            this.Icon = icon;
        }
        public QuestionModel(Text title, PackIconKind kind) : this(title, "", kind) { }
        public QuestionModel() { }
    }
    public delegate void QuestionValidateEventHandler(QuestionModel question, ValidationInfo info);
}
