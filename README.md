# Tizen TV UIControls [![Build Status](http://13.124.0.26:8080/job/Tizen.TV.UIControls/job/Release/badge/icon)](http://13.124.0.26:8080/job/Tizen.TV.UIControls/job/Release/) [![myget](https://img.shields.io/tizen.myget/dotnet/vpre/Tizen.TV.UIControls.svg)](https://tizen.myget.org/feed/dotnet/package/nuget/Tizen.TV.UIControls) [![Codacy Badge](https://api.codacy.com/project/badge/Grade/b441d26bd57c490c820748c5724abda4)](https://www.codacy.com/project/TizenNET/Tizen.TV.UIControls/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=Samsung/Tizen.TV.UIControls&amp;utm_campaign=Badge_Grade_Dashboard)

The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device. The binaries are available via NuGet (package name is Tizen.TV.UIControls.Forms) with the source available here.

## Screen shot
<img src=https://user-images.githubusercontent.com/1029155/42200625-34b8332a-7ecf-11e8-9494-5f97cf4c3e60.gif width=250> <img src=https://user-images.githubusercontent.com/1029155/42200629-3742fb16-7ecf-11e8-82ea-dc8dd5fd9619.gif width=250> <img src=https://user-images.githubusercontent.com/1029155/42200631-3b63edcc-7ecf-11e8-8435-31e12c5ed79e.gif width=250> <img src=https://user-images.githubusercontent.com/1029155/42200633-3d5b9396-7ecf-11e8-91c2-72f3d1003360.gif width=250> <img src=https://user-images.githubusercontent.com/1029155/42200637-4685077c-7ecf-11e8-9984-4c68048da265.gif width=250> <img src=https://user-images.githubusercontent.com/1029155/42200638-489afd3c-7ecf-11e8-981d-8f27169ee8c0.gif width=250>

## Getting Started
### Install package 
#### nuget.exe
```
nuget.exe install Tizen.TV.UIControls -Version 1.0.0
```
#### .csproj
```xml
<PackageReference Include="Tizen.TV.UIControls" Version="1.0.0" />
```
### Use in Xaml
#### Declaring Namespaces for TV.UIControls
``` xml
<ContentPage ... xmlns:tv="clr-namespace:Tizen.TV.UIControls.Forms;assembly=Tizen.TV.UIControls.Forms" ...>
```
### Initialization on Platform code
``` c#
Tizen.TV.UIControls.Forms.UIControls.Init();
//set main window provider
Tizen.TV.UIControls.Forms.UIControls.MainWindowProvider = () => app.MainWindow;
Forms.Init(app);
```

### Guides
 https://samsung.github.io/Tizen.TV.UIControls/guides/Overview.html
### API document
 https://samsung.github.io/Tizen.TV.UIControls/api/index.html
