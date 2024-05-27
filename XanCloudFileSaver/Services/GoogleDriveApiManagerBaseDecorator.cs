namespace XanCloudFileSaver.Services;

public abstract class GoogleDriveApiManagerBaseDecorator(GoogleDriveApiManager googleDriveApiManager)
    : GoogleDriveApiManager
{
    protected readonly GoogleDriveApiManager GoogleDriveApiManager = googleDriveApiManager;
}