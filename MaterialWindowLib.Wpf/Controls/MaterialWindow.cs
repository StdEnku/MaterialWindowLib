namespace MaterialWindowLib.Wpf.Controls;


using System;
using System.Windows;
using System.Windows.Shell;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialWindowLib.Wpf.Properties;

/// <summary>
/// マテリアルデザインを適用したウィンドウ
/// </summary>
public class MaterialWindow : Window
{
    private WindowChrome _windowChrome = new();

    #region コンストラクタ
    /// <summary>
    /// デフォルトコンストラクタ
    /// </summary>
    public MaterialWindow()
    {
        // WindowChromeの設定
        this._windowChrome.ResizeBorderThickness = this.ResizeBorderThickness;
        this._windowChrome.UseAeroCaptionButtons = false;
        this._windowChrome.CaptionHeight = this.TitlebarHeight;
        WindowChrome.SetWindowChrome(this, this._windowChrome);

        // 読み込み時イベントと終了時イベントの登録
        this.Loaded += this.onLoaded;
        this.Closed += this.onClosed;

        // Windowの位置とサイズと状態の読み込み
        this.Height = Settings.Default.MaterialWindowHeight;
        this.Width = Settings.Default.MaterialWindowWidth;
        this.Top = Settings.Default.MaterialWindowTop;
        this.Left = Settings.Default.MaterialWindowLeft;
        this.WindowState = Settings.Default.IsMaterialWindowStateNormal ? WindowState.Normal : WindowState.Maximized;
    }

    static MaterialWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MaterialWindow), new FrameworkPropertyMetadata(typeof(MaterialWindow)));
    }
    #endregion

    #region Loadedイベント発生時の処理
    private void onLoaded(object sender, RoutedEventArgs e)
    {
        // タイトルバー右端のボタンへの参照取得
        var minimizeButton = (Button)this.Template.FindName("MinimizeButton", this);
        var stateChangeButton = (Button)this.Template.FindName("StateChangeButton", this);
        var exitButton = (Button)this.Template.FindName("ExitButton", this);

        // 各種クリックイベントを登録
        minimizeButton.Click += this.onMinimizeButtonClicked;
        stateChangeButton.Click += this.onStateChangeButtonClicked;
        exitButton.Click += this.onExitButtonClicked;
    }
    #endregion

    #region Closedイベント発生時の処理
    private void onClosed(object? sender, EventArgs e)
    {
        // タイトルバー右端のボタンへの参照取得
        var minimizeButton = (Button)this.Template.FindName("MinimizeButton", this);
        var stateChangeButton = (Button)this.Template.FindName("StateChangeButton", this);
        var exitButton = (Button)this.Template.FindName("ExitButton", this);

        // イベントハンドラ解除
        this.Loaded -= this.onLoaded;
        this.Closed -= this.onClosed;
        minimizeButton.Click -= this.onMinimizeButtonClicked;
        stateChangeButton.Click -= this.onStateChangeButtonClicked;
        exitButton.Click -= this.onExitButtonClicked;

        // Windowの位置とサイズと状態の保存
        if (this.WindowState == WindowState.Normal)
        {
            Settings.Default.MaterialWindowHeight = this.Height;
            Settings.Default.MaterialWindowWidth = this.Width;
            Settings.Default.MaterialWindowTop = this.Top;
            Settings.Default.MaterialWindowLeft = this.Left;
            Settings.Default.IsMaterialWindowStateNormal = true;
        }
        else
        {
            Settings.Default.IsMaterialWindowStateNormal = false;
        }
        Settings.Default.Save();
    }
    #endregion

    #region 最小化ボタン押下時の処理
    private void onMinimizeButtonClicked(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }
    #endregion

    #region 通常化/最大化ボタン押下時の処理
    private void onStateChangeButtonClicked(object sender, RoutedEventArgs e)
    {
        var nextState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        this.WindowState = nextState;
    }
    #endregion

    #region 終了ボタン押下時の処理
    private void onExitButtonClicked(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
    #endregion

    #region タイトルバーの高さを指定するための依存関係プロパティ
    /// <summary>
    /// タイトルバーの高さを指定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty TitlebarHeightProperty
        = DependencyProperty.Register(
            "TitlebarHeight",
            typeof(double),
            typeof(MaterialWindow),
            new PropertyMetadata(40d, onTitlebarHeightPropertyChanged)
        );

    /// <summary>
    /// TitlebarHeightProperty依存関係プロパティ用CLRプロパティ
    /// </summary>
    public double TitlebarHeight
    {
        get => (double)this.GetValue(TitlebarHeightProperty);
        set => this.SetValue(TitlebarHeightProperty, value);
    }

    private static void onTitlebarHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is MaterialWindow materialWindow)
        {
            materialWindow._windowChrome.CaptionHeight = (double)e.NewValue;
        }
    }
    #endregion

    #region ボタン内のアイコンサイズを指定するための依存関係プロパティ
    /// <summary>
    /// ボタン内のアイコンサイズを指定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty ButtonIconSizeProperty
        = DependencyProperty.Register(
            "ButtonIconSize",
            typeof(double),
            typeof(MaterialWindow),
            new PropertyMetadata(25d)
        );

    /// <summary>
    /// ButtonIconSizeProperty依存関係プロパティ用CLRプロパティ
    /// </summary>
    public double ButtonIconSize
    {
        get => (double)this.GetValue(ButtonIconSizeProperty);
        set => this.SetValue(ButtonIconSizeProperty, value);
    }
    #endregion

    #region タイトルバー左端のコンテンツ指定用依存関係プロパティ
    /// <summary>
    /// タイトルバー左端のコンテンツ指定用依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty TitlebarLeftContentProperty
        = DependencyProperty.Register(
            "TitlebarLeftContent",
            typeof(object),
            typeof(MaterialWindow),
            new PropertyMetadata(null)
        );

    /// <summary>
    /// TitlebarLeftContentProperty依存関係プロパティ用CLRプロパティ
    /// </summary>
    public object? TitlebarLeftContent
    {
        get => this.GetValue(TitlebarLeftContentProperty);
        set => this.SetValue(TitlebarLeftContentProperty, value);
    }
    #endregion

    #region タイトルバー中央のコンテンツ指定用依存関係プロパティ
    /// <summary>
    /// タイトルバー中央のコンテンツ指定用依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty TitlebarCenterContentProperty
        = DependencyProperty.Register(
            "TitlebarCenterContent",
            typeof(object),
            typeof(MaterialWindow),
            new PropertyMetadata(null)
        );

    /// <summary>
    /// TitlebarCenterContentProperty依存関係プロパティ用CLRプロパティ
    /// </summary>
    public object? TitlebarCenterContent
    {
        get => this.GetValue(TitlebarCenterContentProperty);
        set => this.SetValue(TitlebarCenterContentProperty, value);
    }
    #endregion

    #region タイトルバーの背景用ブラシ依存関係プロパティ
    /// <summary>
    /// タイトルバーの背景用ブラシ依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty TitlebarBackgroundProperty
        = DependencyProperty.Register(
            "TitlebarBackground",
            typeof(Brush),
            typeof(MaterialWindow),
            new PropertyMetadata(Brushes.Transparent)
        );

    /// <summary>
    /// TitlebarBackgroundProperty依存関係プロパティ用CLRプロパティ
    /// </summary>
    public Brush TitlebarBackground
    {
        get => (Brush)this.GetValue(TitlebarBackgroundProperty);
        set => this.SetValue(TitlebarBackgroundProperty, value);
    }
    #endregion

    #region リサイズ用領域の範囲指定用依存関係プロパティ
    /// <summary>
    /// リサイズ用領域の範囲指定用依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty ResizeBorderThicknessProperty
        = DependencyProperty.Register(
            "ResizeBorderThickness",
            typeof(Thickness),
            typeof(MaterialWindow),
            new PropertyMetadata(new Thickness(10, 0, 10, 10), onResizeBorderThicknessPropertyChanged)
        );

    /// <summary>
    /// ResizeBorderThicknessProperty依存関係プロパティ用CLRプロパティ
    /// </summary>
    public Thickness ResizeBorderThickness
    {
        get => (Thickness)this.GetValue(ResizeBorderThicknessProperty);
        set => this.SetValue(ResizeBorderThicknessProperty, value);
    }

    private static void onResizeBorderThicknessPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is MaterialWindow materialWindow)
        {
            materialWindow._windowChrome.ResizeBorderThickness = (Thickness)e.NewValue;
        }
    }
    #endregion

    #region 画面下部のDrawerを表示するかどうか指定するための依存関係プロパティ
    /// <summary>
    /// 画面下部のDrawerを表示するかどうか指定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty IsBottomDrawerShowProperty
        = DependencyProperty.Register(
            "IsBottomDrawerShow",
            typeof(bool),
            typeof(MaterialWindow),
            new PropertyMetadata(false)
        );

    /// <summary>
    /// IsBottomDrawerShowProperty用CLRプロパティ
    /// </summary>
    public bool IsBottomDrawerShow
    {
        get => (bool)this.GetValue(IsBottomDrawerShowProperty);
        set => this.SetValue(IsBottomDrawerShowProperty, value);
    }
    #endregion

    #region 画面下部のDrawerに表示する文字列を指定するための依存関係プロパティ
    /// <summary>
    /// 画面下部のDrawerに表示する文字列を指定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty BottomDrawerTextProperty
        = DependencyProperty.Register(
            "BottomDrawerText",
             typeof(string),
             typeof(MaterialWindow),
             new PropertyMetadata(string.Empty)
        );

    /// <summary>
    /// BottomDrawerTextProperty用CLRプロパティ
    /// </summary>
    public string BottomDrawerText
    {
        get => (string)this.GetValue(BottomDrawerTextProperty);
        set => this.SetValue(BottomDrawerTextProperty, value);
    }
    #endregion

    #region 画面下部のDrawerに表示する文字列のサイズ指定するための依存関係プロパティ
    /// <summary>
    /// 画面下部のDrawerに表示する文字列のサイズ指定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty BottomDrawerTextSizeProperty
        = DependencyProperty.Register(
            "BottomDrawerTextSize",
             typeof(double),
             typeof(MaterialWindow),
             new PropertyMetadata(20d)
        );

    /// <summary>
    /// BottomDrawerTextSizeProperty用CLRプロパティ
    /// </summary>
    public double BottomDrawerTextSize
    {
        get => (double)this.GetValue(BottomDrawerTextSizeProperty);
        set => this.SetValue(BottomDrawerTextSizeProperty, value);
    }
    #endregion
}