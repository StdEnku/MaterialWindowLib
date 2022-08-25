namespace MaterialWindowLib.Demo;

using System;
using System.Windows.Input;

public class DelegateCommand : ICommand
{
    private Action _execute;
    private Func<bool>? _canExecute;
    public event EventHandler? CanExecuteChanged;

    public DelegateCommand(Action execute, Func<bool>? canExecute = null)
    {
        this._execute = execute;
        this._canExecute = canExecute;
    }

    public bool CanExecute(object? parameter)
    {
        var result = this._canExecute is not null ? this._canExecute() : true;
        return result;
    }

    public void RaiseCanExecuteChanged()
    {
        this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Execute(object? parameter)
    {
        this._execute();
    }
}