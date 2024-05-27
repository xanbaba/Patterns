using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using XanCloudFileSaver.Messages;
using XanCloudFileSaver.Services;

namespace XanCloudFileSaver.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty] private BaseViewModel _currentViewModel;

    public MainViewModel(ViewModelFactory currentViewModelFactory)
    {
        _currentViewModel = currentViewModelFactory.CreateViewModel();
        
        WeakReferenceMessenger.Default.Register<ChangeCurrentViewModelMessage>(this, ChangeCurrentViewModel);
    }

    private void ChangeCurrentViewModel(object recipient, ChangeCurrentViewModelMessage message)
    {
        CurrentViewModel = message.ViewModel;
    }
}