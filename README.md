# Xamarin.Forms Extensions for Tizen

## Tizen Theme Common
The Tizen Theme Common is a set of helpful extensions to the Xamarin Forms framework for Tizen devices. It provides various UI controls that can be commonly used in all different Tizen profiles with rich and customizable functionalities. The Nuget package is available with the name of Tizen.Theme.Common.

### Screenshots
<img src=https://user-images.githubusercontent.com/14328614/111264257-8753bf00-866a-11eb-8c70-e4257f11811f.gif width=150> <img src=https://user-images.githubusercontent.com/14328614/111265008-cfbfac80-866b-11eb-92f3-c6123af54adb.gif width=250>
<img src=https://user-images.githubusercontent.com/1029155/96542067-4e423900-12dc-11eb-8d0c-5d97c1b304e5.gif width=250> <img src=https://user-images.githubusercontent.com/14328614/111270455-398f8480-8673-11eb-9016-f35b24c0c328.gif width=250>

### Getting Started
#### Install package
##### .csproj
```xml
  <PackageReference Include="Tizen.Theme.Common" Version="1.1.0-pre5" />
```
#### Initialization on Platform code
```cs
CommonUI.Init(app);

// Or, you can use InitOptions as followings. 
// Make sure it is `Tizen.Theme.Common.InitOptions`. (not Tizen.TV.UIControls.InitOptions)
// CommonUI.Init(new InitOptions(app));

...

// `CommonUI.AddCommonThemeOverlay()` should be called after other's Init including `Forms.Init()` and `UIControls.Init()`
CommonUI.AddCommonThemeOverlay();
```


## Tizen TV UIControls [![Build Status](http://13.124.0.26:8080/job/Tizen.TV.UIControls/job/Release/badge/icon)](http://13.124.0.26:8080/job/Tizen.TV.UIControls/job/Release/) [![myget](https://img.shields.io/tizen.myget/dotnet/vpre/Tizen.TV.UIControls.svg)](https://tizen.myget.org/feed/dotnet/package/nuget/Tizen.TV.UIControls) [![Codacy Badge](https://api.codacy.com/project/badge/Grade/b441d26bd57c490c820748c5724abda4)](https://www.codacy.com/project/TizenNET/Tizen.TV.UIControls/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=Samsung/Tizen.TV.UIControls&amp;utm_campaign=Badge_Grade_Dashboard)

The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device. The binaries are available via NuGet (package name is Tizen.TV.UIControls.Forms) with the source available here.

### Screenshots
<img src=https://user-images.githubusercontent.com/1029155/42200625-34b8332a-7ecf-11e8-9494-5f97cf4c3e60.gif width=250> <img src=https://user-images.githubusercontent.com/1029155/42200629-3742fb16-7ecf-11e8-82ea-dc8dd5fd9619.gif width=250> <img src=https://user-images.githubusercontent.com/1029155/42200631-3b63edcc-7ecf-11e8-8435-31e12c5ed79e.gif width=250> <img src=https://user-images.githubusercontent.com/1029155/42200633-3d5b9396-7ecf-11e8-91c2-72f3d1003360.gif width=250> <img src=https://user-images.githubusercontent.com/1029155/42200637-4685077c-7ecf-11e8-9984-4c68048da265.gif width=250> <img src=https://user-images.githubusercontent.com/1029155/42200638-489afd3c-7ecf-11e8-981d-8f27169ee8c0.gif width=250>

### Getting Started
#### Install package 
##### nuget.exe
```
nuget.exe install Tizen.TV.UIControls -Version 1.1.0
```
##### .csproj
```xml
<PackageReference Include="Tizen.TV.UIControls" Version="1.1.0" />
```
#### Use in Xaml
##### Declaring Namespaces for TV.UIControls
``` xml
<ContentPage ... xmlns:tv="clr-namespace:Tizen.TV.UIControls.Forms;assembly=Tizen.TV.UIControls.Forms" ...>
```
#### Initialization on Platform code
- New way (since 1.1.0-pre2)
``` c#
Forms.Init(app);
// UIControls.Init() should be called after Forms.Init() 
// No MainWindowProvider required for MediaPlayer
UIControls.Init(new InitOptions(app));
```

- Legacy (~ 1.0.0)
``` c#
Forms.Init(app);
UIControls.Init();
//set main window provider
UIControls.MainWindowProvider = () => app.MainWindow;
```

### Guides
 https://samsung.github.io/Tizen.TV.UIControls/guides/Overview.html
### API document
 https://samsung.github.io/Tizen.TV.UIControls/api/index.html
