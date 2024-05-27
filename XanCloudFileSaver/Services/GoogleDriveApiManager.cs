using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using XanCloudFileSaver.Exceptions;
using File = Google.Apis.Drive.v3.Data.File;

namespace XanCloudFileSaver.Services;

public class GoogleDriveApiManager : ICloudFileSaver
{
    private static UserCredential? _userCredential;
    
    public virtual async Task SaveFile(string filePath)
    {
        try
        {
            var credential = GetCredential();
            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = await credential
            });

            var fileMetadata = new File
            {
                Name = filePath
            };
            await using var stream = new FileStream(filePath,
                FileMode.Open);
            var request = service.Files.Create(
                fileMetadata, stream, string.Empty);
            request.Fields = "id";
            await request.UploadAsync();
        }
        catch (Exception)
        {
            throw new GoogleDriveSendingException();
        }
    }

    public virtual async Task UpdateUser()
    {
        Task<UserCredential> credential;
        await using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
        {
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                (await GoogleClientSecrets.FromStreamAsync(stream)).Secrets,
                new[] { DriveService.Scope.Drive },
                Guid.NewGuid().ToString(), CancellationToken.None);
        }

        _userCredential = await credential;
    }

    private async Task<UserCredential> GetCredential()
    {
        if (_userCredential == null)
        {
            await UpdateUser();
        }

        return _userCredential!;
    }
}