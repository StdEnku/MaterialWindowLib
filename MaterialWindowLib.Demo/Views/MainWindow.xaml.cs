namespace MaterialWindowLib.Demo;

using MaterialWindowLib.Wpf.Controls;
using MaterialWindowLib.Demo.Services;
using MaterialWindowLib.Demo.ViewModels;

public partial class MainWindow : MaterialWindow
{
    public MainWindow()
    {
        InitializeComponent();
        var applicationShutdownService = new ApplicationShutdownService();
        this.DataContext = new MainWindowViewModel(applicationShutdownService);
    }
}
