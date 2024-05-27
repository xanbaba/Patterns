using XanCloudFileSaver.ViewModels;

namespace XanCloudFileSaver.Messages;

public class ChangeCurrentViewModelMessage(BaseViewModel viewModel) : Message
{
    public BaseViewModel ViewModel { get; } = viewModel;
}