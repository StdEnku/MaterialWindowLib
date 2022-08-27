namespace MaterialWindowLib.Demo.Commands;

using System;
using System.Windows.Input;

public class DelegateCommand : ICommand
{
    private Action _execute;
    private Func<bool>? _canExecute;
    public event EventHandler? CanExecuteChanged;

    public DelegateCommand(Action execute, Func<bool>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter)
    {
        var result = _canExecute is not null ? _canExecute() : true;
        return result;
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Execute(object? parameter)
    {
        _execute();
    }
}