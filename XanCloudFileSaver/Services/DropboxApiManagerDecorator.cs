using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using Dropbox.Api;
using Dropbox.Api.Files;

// прикольная штука
using Microsoft.Extensions.Configuration;
using XanCloudFileSaver.Exceptions;

namespace XanCloudFileSaver.Services;

public class DropboxApiManagerDecorator : GoogleDriveApiManagerBaseDecorator
{
    private static HttpListener? _listener;

    private const string RedirectUri = "http://localhost:5000/";
    private const string AppSettingsFilePath = "Services/AppSettings.json";

    private readonly string? _appKey;
    private readonly string? _appSecret;

    public DropboxApiManagerDecorator(GoogleDriveApiManager googleDriveApiManager) : base(googleDriveApiManager)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile(AppSettingsFilePath)
            .Build();

        // Get a configuration section
        var section = config.GetSection("Settings");
        _appKey = section["AppKey"];
        _appSecret = section["AppSecret"];
    }
    
    public override async Task SaveFile(string filePath)
    {

        try
        {
            if (_appKey == null || _appSecret == null)
            {
                throw new ConfigurationErrorsException("Could not load configuration from file");
            }


            // Step 1: Start the local HTTP server
            if (_listener == null)
            {
                _listener = new HttpListener();
                _listener.Prefixes.Add(RedirectUri);
            }

            _listener.Start();

            // Step 2: Redirect user to Dropbox for authorization
            Uri authorizeUri =
                DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Code, _appKey, new Uri(RedirectUri));
            OpenBrowser(authorizeUri.ToString());

            // Step 3: Wait for the incoming request with the authorization code
            var context = await _listener.GetContextAsync();
            var accessToken = await HandleRequest(context, _appKey, _appSecret, RedirectUri);

            using (var dbx = new DropboxClient(accessToken))
            {
                await dbx.Users.GetCurrentAccountAsync();
                await Upload(dbx, filePath);
            }

            _listener.Stop();
        }
        catch (Exception)
        {
            throw new DropboxSendingException();
        }
        
        await GoogleDriveApiManager.SaveFile(filePath);
    }
    
    private static async Task<string?> HandleRequest(HttpListenerContext context, string appKey, string appSecret, string redirectUri)
    {
        var request = context.Request;
        var response = context.Response;

        // Check if the request contains the authorization code
        var code = request.QueryString["code"];
        if (!string.IsNullOrEmpty(code))
        {
            // Exchange the authorization code for an access token
            var tokenResult = await DropboxOAuth2Helper.ProcessCodeFlowAsync(code, appKey, appSecret, redirectUri);
            var accessToken = tokenResult.AccessToken;

            // Respond to the client
            const string responseString = "<html><body>Authorization successful. You can close this window.</body></html>";
            var buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var output = response.OutputStream;
            await output.WriteAsync(buffer);
            output.Close();
            
            return accessToken;
        }
        else
        {
            // Handle missing or invalid authorization code
            const string responseString = "<html><body>Authorization failed. No code received.</body></html>";
            var buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var output = response.OutputStream;
            await output.WriteAsync(buffer);
            output.Close();
        }

        return null;
    }
    
    private async Task Upload(DropboxClient dbx, string filePath)
    {
        await using var mem = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        await dbx.Files.UploadAsync(
            "/XanCloud File Saver/" + Path.GetFileName(filePath),
            WriteMode.Overwrite.Instance,
            body: mem);
    }
    
    private static void OpenBrowser(string url)
    {
        // Try to open the URL in the default browser
        var psi = new ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        };
        Process.Start(psi);
    }
}