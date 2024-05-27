using XanCloudFileSaver.ViewModels;

namespace XanCloudFileSaver.Services;

public abstract class ViewModelFactory
{
    public abstract BaseViewModel CreateViewModel();
}