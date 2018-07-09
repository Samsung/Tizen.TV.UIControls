# Tizen TV UIControls
The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device. The binaries are available via NuGet (package name is Tizen.TV.UIControls.Forms) with the source available here.

## Screen shot
<img src="https://user-images.githubusercontent.com/1029155/42200625-34b8332a-7ecf-11e8-9494-5f97cf4c3e60.gif" width="250"> <img src="https://user-images.githubusercontent.com/1029155/42200629-3742fb16-7ecf-11e8-82ea-dc8dd5fd9619.gif" width="250"> <img src="https://user-images.githubusercontent.com/1029155/42200631-3b63edcc-7ecf-11e8-8435-31e12c5ed79e.gif" width="250"> <img src="https://user-images.githubusercontent.com/1029155/42200633-3d5b9396-7ecf-11e8-91c2-72f3d1003360.gif" width="250"> <img src="https://user-images.githubusercontent.com/1029155/42200637-4685077c-7ecf-11e8-9984-4c68048da265.gif" width="250"> <img src="https://user-images.githubusercontent.com/1029155/42200638-489afd3c-7ecf-11e8-981d-8f27169ee8c0.gif" width="250">


## Build Status
 [![Build Status](http://13.124.0.26:8080/job/Tizen.TV.UIControls/job/Release/badge/icon)](http://13.124.0.26:8080/job/Tizen.TV.UIControls/job/Release/)
## Packages
[![myget](https://img.shields.io/tizen.myget/dotnet/vpre/Tizen.TV.UIControls.svg)](https://tizen.myget.org/feed/dotnet/package/nuget/Tizen.TV.UIControls)
## Getting Started
### Intasll package 
#### nuget.ext
```
nuget.exe install Tizen.TV.UIControls -Version 1.0.0-beta -Source https://tizen.myget.org/F/dotnet/api/v3/index.json
```
#### .csproj
```xml
<PackageReference Include="Tizen.TV.UIControls" Version="1.0.0-beta" />
```
### Use in Xaml
#### Declaring Namespaces for TV.UIControls
``` xml
<ContentPage ... xmlns:tv="clr-namespace:Tizen.TV.UIControls.Forms;assembly=Tizen.TV.UIControls.Forms" ...>
```
### Initialization on Platform code
``` c#
Tizen.TV.UIControls.Forms.Renderer.UIControls.PreInit();
global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
Tizen.TV.UIControls.Forms.Renderer.UIControls.PostInit();
```
