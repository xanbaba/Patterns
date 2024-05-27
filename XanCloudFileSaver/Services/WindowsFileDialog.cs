using Microsoft.Win32;

namespace XanCloudFileSaver.Services;

public class WindowsFileDialog : IFileDialogManager
{
    public string? ShowOpenFileDialog(object? filter)
    {
        var openFileDialog = new OpenFileDialog();
        if (filter != null)
        {
            if (filter is not string filterString)
            {
                throw new InvalidCastException($"parameter {nameof(filter)} must be string");
            }

            openFileDialog.Filter = filterString;
        }

        var dialogResult = openFileDialog.ShowDialog();
        return dialogResult.HasValue && dialogResult.Value ? openFileDialog.FileName : null;
    }
}