using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EDGW.Logging;
using OMCCore.Globalization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace OMCC.Plugins.UserManager.UI
{
    public abstract partial class SimpleUserLoginPageViewModel : ObservableObject
    {
        internal SimpleUserLoginPage? LoginPage { get; set; }
        static Logger logger = new Logger("Simple User Login Page", nameof(SimpleUserLoginPage));
        public ObservableCollection<QuestionModel> Questions { get; } = new ObservableCollection<QuestionModel>();
        [ObservableProperty] Text? title;
        [ObservableProperty] bool isLoading = true;
        [ObservableProperty] bool isError = false;
        [ObservableProperty] string errorMessage = "";
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))][ObservableProperty] bool canLogin = false;
        
        public SimpleUserLoginPageViewModel()
        {
            Questions.CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action== NotifyCollectionChangedAction.Add)
            {
                foreach (QuestionModel q in e.NewItems ?? new List<QuestionModel>())
                {
                    q.Parent = this;
                    q.ValidationInfoChangedEvent += OnQuestionsValidated;
                    q.ValidationInfo = Validate(q);
                }
            }
        }

        private void OnQuestionsValidated(QuestionModel sender, ValidationInfo info)
        {
            bool allow = true;
            if (info.IsValid)
            {
                foreach(var m in this.Questions)
                {
                    if (m != sender)
                    {
                        if (!m.ValidationInfo.IsValid)
                        {
                            allow = false;
                        }
                    }
                }
            }
            else
            {
                allow = false;
            }
            CanLogin = allow;
        }
        [RelayCommand(CanExecute =nameof(CanLogin))]
        public void Login()
        {
            try
            {
                IsLoading = true;
                IsError = false;
                var user = CreateUser();
                if (LoginPage != null)
                {
                    LoginPage.RaiseCallbackEvent(true, user);
                }
            }catch(Exception ex)
            {
                IsError = true;
                ErrorMessage = ex.Message;
                logger.error("Failed to login.", ex);
            }
            finally
            {
                IsLoading = false;
            }
        }
        public abstract User? CreateUser();
        public virtual ValidationInfo Validate(QuestionModel model)
        {
            return new ValidationInfo(true);
        }
    }
}
