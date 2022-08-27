namespace MaterialWindowLib.Wpf.Commands;

using System;
using System.Windows.Input;
using System.Windows;

internal class ExitButtonCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        Application.Current.Shutdown();
    }
}