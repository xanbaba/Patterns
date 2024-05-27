using XanCloudFileSaver.ViewModels;

namespace XanCloudFileSaver.Services;

public class SaveFileViewModelFactory : ViewModelFactory
{
    private static BaseViewModel? _viewModel;

    public override BaseViewModel CreateViewModel()
    {
        return _viewModel ??= new SaveFileViewModel(new SettingsViewModelFactory(), new WindowsFileDialog(),
            new DropboxApiManagerDecorator(new GoogleDriveApiManager()));
    }
}