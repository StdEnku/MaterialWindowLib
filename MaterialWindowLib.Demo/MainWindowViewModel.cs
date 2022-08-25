namespace MaterialWindowLib.Demo;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

internal class MainWindowViewModel : INotifyPropertyChanged
{
    #region INotifyPropertyChangedの実装
    public event PropertyChangedEventHandler? PropertyChanged;

    private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
    {   
        storage = value;
        this.RaisePropertyChanged(propertyName);
    }
    #endregion

    #region BottomDrawer操作用プロパティ
    private bool _isBottomDrawerShow = false;
    public bool IsBottomDrawerShow
    {
        get => this._isBottomDrawerShow;
        set => this.SetProperty(ref this._isBottomDrawerShow, value);
    }

    private string _bottomDrawerText = string.Empty;
    public string BottomDrawerText
    {
        get => this._bottomDrawerText;
        set => this.SetProperty(ref this._bottomDrawerText, value);
    }
    #endregion

    #region Loaded時実行コマンド
    public ICommand LoadedCommand { get; private set; }
    private async void loaded()
    {
        this.BottomDrawerText = "Now Launching. prease wait!!!";
        this.IsBottomDrawerShow = true;
        await Task.Delay(3000);
        this.IsBottomDrawerShow = false;
    }
    #endregion

    public MainWindowViewModel()
    {
        this.LoadedCommand = new DelegateCommand(this.loaded);
    }
}