# Tizen TV UIControls 

<img src="https://github.com/Samsung/Tizen.TV.UIControls/blob/d0ec169fb97a0ece318e4990c86a0c65bf15db5c/assets/tvuicontrols_rgb_64_64.png"/>

- [Introduction](#introduction)
- [Controls](#controls)
- [Prerequisite](#prerequisite)
- [Getting Started](#getting-started)
- [Samples](#samples)

## Introduction
The Tizen TV UI Controls, aka "TV UI", is a set of helpful extensions of the .NET MAUI especially for Tizen TV.<br>
The aim of the TV UI Controls project is an open source software to motivate and help software developer to create Tizen TV app (by using .NET MAUI) more easily and efficiently. The binaries are available via NuGet (package name **Tizen.TV.UIControls**)<br>

ℹ️ _.NET MAUI was designed to build multi-platform apps, but this project are only worked on the Samsung Smart TV products that support Tizen .NET._ 

## Controls
We provides useful UI controls that fit on a TV screen and interact with remote controller.

- [MediaPlayer](https://samsung.github.io/Tizen.TV.UIControls/guides/MediaPlayer_Introduction.html) :  Provides functionality of playing multimedia inclduing related view components that display video stream.
- [RecycleItemsView](https://samsung.github.io/Tizen.TV.UIControls/guides/RecycleItemsView_Introduction.html) : A view that takes in a list of user objects and produces views for each of them to be displayed. Especially, it can be used when the data is displayed in the same view template. It reuses the templated view when it is out of sight.
- [InputEvents](https://samsung.github.io/Tizen.TV.UIControls/guides/InputEvents.html) : Helps developers to handle the remote control events that are emitted from TV devices.
- [DrawerLayout](https://samsung.github.io/Tizen.TV.UIControls/guides/DrawerLayout.html) : Is a kind of Layout that acts like a MasterDetailPage. It has a drawer part that interactively pull and push.
- [FocusFrame](https://samsung.github.io/Tizen.TV.UIControls/guides/FocusFrame.html) : Helps developers to decorate a focused view. It contain a view to represent and if it got a focus, backgroud color of FocusFrame is changed.
- [ContentButton](https://samsung.github.io/Tizen.TV.UIControls/guides/ContentButton.html) : Is a kind of ContentView that contains a single child element (called Content) and is typically used for custom, reusable controls.
- [ContentPopup](https://samsung.github.io/Tizen.TV.UIControls/guides/ContentPopup.html) : Is a kind of ContentView that contains a single child element (called Content) and allows to open it as a popup. 
- [AnimatedNavigationPage](https://samsung.github.io/Tizen.TV.UIControls/guides/AnimatedNavigationPage.html) : Is a kind of NativationView that support page tranisition animation when the page is popped or pushed.

<img src=https://user-images.githubusercontent.com/1029155/42200625-34b8332a-7ecf-11e8-9494-5f97cf4c3e60.gif width=250> <img src=https://user-images.githubusercontent.com/1029155/42200629-3742fb16-7ecf-11e8-82ea-dc8dd5fd9619.gif width=250> <img src=https://user-images.githubusercontent.com/1029155/42200631-3b63edcc-7ecf-11e8-8435-31e12c5ed79e.gif width=250> <img src=https://user-images.githubusercontent.com/1029155/42200633-3d5b9396-7ecf-11e8-91c2-72f3d1003360.gif width=250> <img src=https://user-images.githubusercontent.com/1029155/42200637-4685077c-7ecf-11e8-9984-4c68048da265.gif width=250> <img src=https://user-images.githubusercontent.com/1029155/42200638-489afd3c-7ecf-11e8-981d-8f27169ee8c0.gif width=250> <img src=https://user-images.githubusercontent.com/14328614/111265008-cfbfac80-866b-11eb-92f3-c6123af54adb.gif width=250> <img src=https://user-images.githubusercontent.com/1029155/96542067-4e423900-12dc-11eb-8d0c-5d97c1b304e5.gif width=250> <img src=https://user-images.githubusercontent.com/14328614/111270455-398f8480-8673-11eb-9016-f35b24c0c328.gif width=250>


## Prerequisite
- An environment that has been configured for .NET MAUI development. For more information, see [Install latest .NET 6](https://docs.microsoft.com/ko-kr/dotnet/maui/get-started/installation#install-latest-net-6-preview).
- An environment that has been configured for Tizen .NET development. For more information, see [Install Tizen workload for .NET 6](https://github.com/Samsung/Tizen.NET/wiki/Installing-Tizen-.NET-Workload). 

ℹ️ _Tizen emulators and devices that support .NET6 have not yet been officially released, and we will announce a binary for testing soon._

## Getting Started
- [Quick Start](docs/index.md)
- The [API Reference](https://samsung.github.io/Tizen.TV.UIControls/api/index.html) is available on the web to browse.
- The [Developers Guide](https://samsung.github.io/Tizen.TV.UIControls/guides/Overview.html) is available on the web to browse.

## Samples
<table>
  <tr>
    <th></th>
    <th>Screenshot</th>
    <th>Description</th>
  </tr>
  <tr>
    <td>TV UI Gallery</td>
    <td><img src="https://user-images.githubusercontent.com/1029134/168060727-8316ed65-f7ff-4c15-ba70-79e0d9351d2b.png" alt="widgets"></td>
    <td>This application was created to check the basic behavior of TV UI Controls. It contains all the controls of the TV UI API, and the functions of each control consist of one TC or several TCs depending on the characteristics of the control.<br>
    <a href="https://github.com/Samsung/Tizen.TV.UIControls/tree/maui-migrate/sample/Sample">Source Code</a></td>
  </tr>
  <tr>
    <td>TMDb</td>
    <td><img src="https://user-images.githubusercontent.com/1029134/45856544-e5500600-bd8f-11e8-8579-ba4bb1db7a9a.png" alt="widgets"></td>
    <td>This app shows the list of most popular and highest voted movies as per the [TMDb website](https://www.themoviedb.org). The data is current as of present. <br>
    <a href="https://github.com/Samsung/Tizen.TV.UIControls/tree/maui-migrate/sample/TMDb">Source Code</a></td>
  </tr>     
</table>
