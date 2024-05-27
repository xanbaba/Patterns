using XanCloudFileSaver.ViewModels;

namespace XanCloudFileSaver.Services;

public class SettingsViewModelFactory : ViewModelFactory
{
    public override BaseViewModel CreateViewModel()
    {
        return new SettingsViewModel(new SaveFileViewModelFactory(), new DropboxApiManagerDecorator(new GoogleDriveApiManager()));
    }
}