using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using XanCloudFileSaver.Messages;
using XanCloudFileSaver.Services;

namespace XanCloudFileSaver.ViewModels;

public partial class SettingsViewModel(ViewModelFactory viewModelFactory, ICloudFileSaver fileSaver) : BaseViewModel
{
    [RelayCommand]
    private void Back()
    {
        WeakReferenceMessenger.Default.Send(new ChangeCurrentViewModelMessage(viewModelFactory.CreateViewModel()));
    }

    [RelayCommand]
    private void ChangeGoogleDriveAccount()
    {
        fileSaver.UpdateUser();
    }
}