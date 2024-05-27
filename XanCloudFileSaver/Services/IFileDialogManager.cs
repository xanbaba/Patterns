namespace XanCloudFileSaver.Services;

public interface IFileDialogManager
{
    public string? ShowOpenFileDialog(object? filter);
}