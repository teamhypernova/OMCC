using CommunityToolkit.Mvvm.ComponentModel;
using OMCCore.Globalization;

namespace OMCCore.Core.Tasks
{
    public partial class OTaskItem : ObservableObject
    {
        public OTaskView TaskView { get; set; }
        [ObservableProperty] double progress = 0;
        [ObservableProperty] double maxProgress = 100;
        [ObservableProperty] int weight = 0;
        [ObservableProperty] Text? title;
        [ObservableProperty] bool isIntermediate = false;
        [ObservableProperty] bool isCompleted = false;
        partial void OnIsCompletedChanged(bool value)
        {
            if (value)
            {
                Progress = MaxProgress;
            }
        }
        partial void OnProgressChanged(double value)
        {
            TaskView.RecalcProgress();
        }
        partial void OnMaxProgressChanged(double value)
        {
            TaskView.RecalcProgress();
        }
        partial void OnWeightChanged(int value)
        {
            TaskView.RecalcProgress();
        }
        public void Complete()
        {
            IsCompleted = true;
        }
        public OTaskItem(OTaskView taskView, Text? title, int weight = 1)
        {
            TaskView = taskView;
            Title = title;
            Weight = weight;
        }
        public OTaskItem(OTaskView taskView, Text? title, bool isintermediate = false, int weight = 1)
        {
            TaskView = taskView;
            Title = title;
            Weight = weight;
            IsIntermediate = isintermediate;
        }
    }
}
