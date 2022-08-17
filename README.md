![img1](./img/img1.png)

## 概要

本ライブラリはWPFにおいてマテリアルデザインを取り入れたタイトルバーを含む
ウィンドウを提供するライブラリである。

導入すると下記コードのようにMaterialWindow要素を使用できるようになる

### App.xamlのサンプルコード

```xaml
<Application ~省略~
             xmlns:materialDesign="http://materialdesigninxaml.net/winfx/xaml/themes">

    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <materialDesign:BundledTheme BaseTheme="Dark" PrimaryColor="DeepPurple" SecondaryColor="Lime" />
                <ResourceDictionary Source="pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```



### MainWinodwのサンプルコード

```xaml
<mw:MaterialWindow ~省略~
                   xmlns:mw="http://StdEnku/MaterialWindowLib/Wpf">

    <mw:MaterialWindow.TitlebarLeftContent>
        <Label Content="Hoge" FontSize="20" />
    </mw:MaterialWindow.TitlebarLeftContent>
    
    <mw:MaterialWindow.TitlebarCenterContent>
        <Label Content="Fuga" FontSize="20" />
    </mw:MaterialWindow.TitlebarCenterContent>

    <Viewbox>
        <Label Content="Hello World" />
    </Viewbox>
</mw:MaterialWindow>
```

### 結果

![img2](./img/img2.png)

## MaterialWindowクラスの概要

### プロパティ

| プロパティ名          | 型        | 備考                                                         | 規定値                   |
| --------------------- | --------- | ------------------------------------------------------------ | ------------------------ |
| TitlebarHeight        | double    | タイトルバーの高さを指定するための依存関係プロパティ         | 40                       |
| ButtonIconSize        | double    | ボタン内のアイコンサイズを指定するための依存関係プロパティ   | 25                       |
| TitlebarLeftContent   | object    | タイトルバー左端のコンテンツ指定用依存関係プロパティ         | null                     |
| TitlebarCenterContent | object    | タイトルバー中央のコンテンツ指定用依存関係プロパティ         | null                     |
| TitlebarBackground    | Brush     | タイトルバーの背景用ブラシ依存関係プロパティ                 | PrimaryHueMidBrush       |
| ResizeBorderThickness | Thickness | リサイズ用領域の範囲指定用依存関係プロパティ                 | Thickness(10, 0, 10, 10) |
| IsBottomDrawerShow    | bool      | 画面下部のDrawerを表示するかどうか指定するための依存関係プロパティ | false                    |
| BottomDrawerText      | string    | 画面下部のDrawerに表示する文字列を指定するための依存関係プロパティ | string.Empty             |
| BottomDrawerTextSize  | double    | 画面下部のDrawerに表示する文字列のサイズ指定するための依存関係プロパティ | 20                       |

## 画面下部のDrawerの概要

![img3](./img/img3.gif)

MaterialWindowクラスは上記画像のように画面下部にDrawerを保持しており、
BottomDrawerTextプロパティにテキストの内容を指定して
BottomDrawerTextSizeプロパティでテキストサイズを指定し、
IsBottomDrawerShowプロパティで表示のON/OFFを切り替える事が可能である。