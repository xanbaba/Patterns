namespace XanCloudFileSaver.Services;

public interface ICloudFileSaver
{
    public Task SaveFile(string filePath);
    public Task UpdateUser();
}