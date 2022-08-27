namespace MaterialWindowLib.Demo.Services;

using System.Windows;

internal class ApplicationShutdownService : IApplicationShutdownService
{
    public void Shutdown()
    {
        Application.Current.Shutdown();
    }
}
