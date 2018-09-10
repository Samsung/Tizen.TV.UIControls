
# Troubleshooter

Install `Tizen.TV.UIControls` package to your TV application to resolve the following limitations. <br/>

The `Tizen.TV.UIControls` package will replace the default renderers for `Xamarin.Forms.Entry` and `Xamarin.Forms.Editor`
to resolve the issues described below. 

> The following limitations are reproduced on [2018 TV models](https://developer.samsung.com/tv/develop/specifications/tv-model-groups) that use Tizen 4.0 platform.

## Limitations

### Entry

When `Xamarin.Forms.Entry` gets the focus, the Input Panel is showed automatically on the screen to allow a user to enter the text.
However, the Input Panel: <br/>
  - is not closed, when `Done` or `Cancel` key is pressed.
  - is not closed, when `Back key` on the remote control is pressed.
  - does not invoke the `Entry.Completed` event, when `Done` key is pressed.
	
	
### Editor

When `Xamarin.Forms.Editor` gets the focus, the Input Panel is showed automatically on the screen to allow a user to enter the text.
However, the Input Panel: <br/>
  - is not closed, when `Done` or `Cancel` key is pressed.
  - is not closed, when `Back key` on the remote control is pressed.
  - does not invoke the `Entry.Completed` event, when `Done` key is pressed.
	

