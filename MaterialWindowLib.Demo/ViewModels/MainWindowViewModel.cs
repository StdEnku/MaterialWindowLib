namespace MaterialWindowLib.Demo.ViewModels;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using MaterialWindowLib.Demo.Commands;
using MaterialWindowLib.Demo.Services;

internal class MainWindowViewModel : INotifyPropertyChanged
{
    IApplicationShutdownService _applicationShutdownService;

    #region INotifyPropertyChangedの実装
    public event PropertyChangedEventHandler? PropertyChanged;

    private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
    {
        storage = value;
        RaisePropertyChanged(propertyName);
    }
    #endregion

    #region BottomDrawer操作用プロパティ
    private bool _isBottomDrawerShow = false;
    public bool IsBottomDrawerShow
    {
        get => _isBottomDrawerShow;
        set => SetProperty(ref _isBottomDrawerShow, value);
    }

    private string _bottomDrawerText = string.Empty;
    public string BottomDrawerText
    {
        get => _bottomDrawerText;
        set => SetProperty(ref _bottomDrawerText, value);
    }
    #endregion

    #region Loaded時実行コマンド
    public ICommand LoadedCommand { get; private set; }
    private async void loaded()
    {
        BottomDrawerText = "Now Launching. prease wait";
        IsBottomDrawerShow = true;
        await Task.Delay(3000);
        IsBottomDrawerShow = false;
    }
    #endregion

    #region 終了ボタン押下時コマンド
    public ICommand ExitButtonClickedCommand { get; private set; }
    private async void exitButtonClicked()
    {
        BottomDrawerText = "Now Finalizing. prease wait";
        IsBottomDrawerShow = true;
        await Task.Delay(3000);
        IsBottomDrawerShow = false;
        this._applicationShutdownService.Shutdown();
    }
    #endregion

    public MainWindowViewModel(IApplicationShutdownService applicationShutdownService)
    {
        LoadedCommand = new DelegateCommand(loaded);
        ExitButtonClickedCommand = new DelegateCommand(exitButtonClicked);
        this._applicationShutdownService = applicationShutdownService;
    }
}