using System.Configuration;
using System.Data;
using System.Windows;
using XanCloudFileSaver.Services;
using XanCloudFileSaver.ViewModels;
using XanCloudFileSaver.Views;

namespace XanCloudFileSaver;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        MainWindow = new MainWindow
        {
            DataContext = new MainViewModel(new SaveFileViewModelFactory())
        };

        MainWindow.ShowDialog();
    }
}