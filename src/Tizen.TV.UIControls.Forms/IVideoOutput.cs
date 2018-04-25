using System;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    public enum VideoOuputType
    {
        Overlay,
        Buffer,
    }

    public interface IVideoOutput
    {
        VisualElement MediaView { get; }
        VideoOuputType OuputType { get; }
    }

    public interface IOverlayOutput : IVideoOutput
    {
        Rectangle OverlayArea { get; }
        event EventHandler AreaUpdated;
    }
}
