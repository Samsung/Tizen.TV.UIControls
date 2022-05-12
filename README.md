# .NET MAUI Extensions for Tizen

## Tizen TV UIControls 
The Tizen TV UIControls is a set of helpful extensions to the .NET MAUI for the Samsung TV device. The binaries are available via NuGet (package name is Tizen.TV.UIControls.Maui) with the source available here.

### Screenshots
<img src=https://user-images.githubusercontent.com/1029155/42200625-34b8332a-7ecf-11e8-9494-5f97cf4c3e60.gif width=250> <img src=https://user-images.githubusercontent.com/1029155/42200629-3742fb16-7ecf-11e8-82ea-dc8dd5fd9619.gif width=250> <img src=https://user-images.githubusercontent.com/1029155/42200631-3b63edcc-7ecf-11e8-8435-31e12c5ed79e.gif width=250> <img src=https://user-images.githubusercontent.com/1029155/42200633-3d5b9396-7ecf-11e8-91c2-72f3d1003360.gif width=250> <img src=https://user-images.githubusercontent.com/1029155/42200637-4685077c-7ecf-11e8-9984-4c68048da265.gif width=250> <img src=https://user-images.githubusercontent.com/1029155/42200638-489afd3c-7ecf-11e8-981d-8f27169ee8c0.gif width=250>

### Getting Started
#### Install package 
##### nuget.exe
```
nuget.exe install Tizen.TV.UIControls -Version 1.0.0-rc3
```
##### .csproj
```xml
<PackageReference Include="Tizen.TV.UIControls" Version="1.0.0-rc3" />
```
#### Use in Xaml
##### Declaring Namespaces for TV.UIControls
``` xml
<ContentPage ... xmlns:tv="clr-namespace:Tizen.TV.UIControls.Maui;assembly=Tizen.TV.UIControls.Maui" ...>
```

### Guides
 https://samsung.github.io/Tizen.TV.UIControls/guides/Overview.html
### API document
 https://samsung.github.io/Tizen.TV.UIControls/api/index.html

----

## Tizen Theme Common
The Tizen Theme Common is a set of helpful extensions to the .NET MAUI for Tizen devices. It provides various UI controls that can be commonly used in all different Tizen profiles with rich and customizable functionalities. The Nuget package is available with the name of Tizen.Theme.Common.Maui.

### Screenshots
<img src=https://user-images.githubusercontent.com/14328614/111265008-cfbfac80-866b-11eb-92f3-c6123af54adb.gif width=250> <img src=https://user-images.githubusercontent.com/1029155/96542067-4e423900-12dc-11eb-8d0c-5d97c1b304e5.gif width=250> <img src=https://user-images.githubusercontent.com/14328614/111270455-398f8480-8673-11eb-9016-f35b24c0c328.gif width=250>

### Getting Started
#### Install package
##### nuget.exe
```
nuget.exe install Tizen.Theme.Common.Maui -Version 1.0.0-rc3
```
##### .csproj
```xml
<PackageReference Include="Tizen.Theme.Common.Maui" Version="1.0.0-rc3" />
```
