using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using XanCloudFileSaver.Exceptions;
using XanCloudFileSaver.Messages;
using XanCloudFileSaver.Services;
using Timer = System.Timers.Timer;

namespace XanCloudFileSaver.ViewModels;

public partial class SaveFileViewModel(ViewModelFactory viewModelFactory, IFileDialogManager fileDialogManager, ICloudFileSaver fileSaver) : BaseViewModel
{
    [ObservableProperty] private string _selectedFileName = string.Empty;
    [ObservableProperty] private string _errorMessage = string.Empty;

    private const string GoogleDriveSendingErrorMessage = "An error occured while sending the file to Google Drive";
    private const string DropboxSendingErrorMessage = "An error occured while sending the file to Dropbox";
    
    [RelayCommand]
    private void OpenSettingsView()
    {
        WeakReferenceMessenger.Default.Send(new ChangeCurrentViewModelMessage(viewModelFactory.CreateViewModel()));
    }

    [RelayCommand]
    private void ShowOpenFileDialog()
    {
        var fileName = fileDialogManager.ShowOpenFileDialog(null);
        if (fileName == null)
        {
            return;
        }

        SelectedFileName = fileName;
    }

    [RelayCommand]
    private async Task Send()
    {
        if (!string.IsNullOrWhiteSpace(SelectedFileName))
        {
            try
            {
                await fileSaver.SaveFile(SelectedFileName);
            }
            catch (GoogleDriveSendingException)
            {
                ShowErrorMessage(GoogleDriveSendingErrorMessage);
            }
            catch (DropboxSendingException)
            {
                ShowErrorMessage(DropboxSendingErrorMessage);
            }
        }
    }

    private void ShowErrorMessage(string message)
    {
        var timer = new Timer();
        timer.AutoReset = false;
        timer.Interval = 3000;
        timer.Elapsed += (_, _) =>
        {
            ErrorMessage = string.Empty;
        };
        ErrorMessage = message;
        timer.Start();
    }
}