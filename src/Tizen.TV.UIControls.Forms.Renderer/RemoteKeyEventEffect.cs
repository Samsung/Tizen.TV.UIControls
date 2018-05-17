using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElmSharp;
using Tizen.TV.UIControls.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ResolutionGroupName("TizenTVUIControl")]
[assembly: ExportEffect(typeof(RemoteKeyEventEffect), "RemoteKeyEventEffect")]
namespace Tizen.TV.UIControls.Forms
{
    class RemoteKeyEventEffect : PlatformEffect
    {
        EcoreKeyEvents _ecoreKeyEvents = EcoreKeyEvents.Instance;

        // TODO: Test Multipage
        Page currentPage
        {
            get
            {
                var page = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
                if (!(Element is NavigationPage))
                {
                    while (page is NavigationPage)
                    {
                        page = (page as NavigationPage).CurrentPage;
                    }
                }
                if (!(Element is TabbedPage))
                {
                    while (page is TabbedPage)
                    {
                        page = (page as TabbedPage).CurrentPage;
                    }
                }
                return page;
            }
        }

        protected override void OnAttached()
        {
            try
            {
                if (Element is Page)
                {
                    _ecoreKeyEvents.KeyDown += Page_keyDown;
                    _ecoreKeyEvents.KeyUp += Page_keyUp;
                }
                else
                {
                    Control.KeyDown += Control_KeyDown;
                    Control.KeyUp += Control_KeyUp;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to attach the effect : " + e);
            }
        }

        protected override void OnDetached()
        {
            if (Element is Page)
            {
                _ecoreKeyEvents.KeyDown -= Page_keyDown;
                _ecoreKeyEvents.KeyUp -= Page_keyUp;
            }
            else
            {
                Control.KeyDown -= Control_KeyDown;
                Control.KeyUp -= Control_KeyUp;
            }
        }

        void Page_keyDown(object sender, EcoreKeyEventArgs e)
        {
            InvokeActionAndEvent(RemoteControlKeyTypes.KeyDown, e.KeyName);
        }

        void Page_keyUp(object sender, EcoreKeyEventArgs e)
        {
            InvokeActionAndEvent(RemoteControlKeyTypes.KeyUp, e.KeyName);
        }

        void Control_KeyDown(object sender, EvasKeyEventArgs e)
        {
            InvokeActionAndEvent(RemoteControlKeyTypes.KeyDown, e.KeyName);
        }

        void Control_KeyUp(object sender, EvasKeyEventArgs e)
        {
            InvokeActionAndEvent(RemoteControlKeyTypes.KeyUp, e.KeyName);
        }

        RemoteControlKeyEventArgs CreateArgs(RemoteControlKeyTypes keyType, string keyName)
        {
            if (int.TryParse(keyName, out int result))
            {
                if (result >= 0 && result <= 9)
                    keyName = keyName.Insert(0, "NUM");
            }
            try
            {
                RemoteControlKeyNames key = (RemoteControlKeyNames)Enum.Parse(typeof(RemoteControlKeyNames), keyName);
                RemoteControlKeyEventArgs newArgs = new RemoteControlKeyEventArgs(keyType, key);
                return newArgs;
            }
            catch
            {
                Console.WriteLine("Unkown Key event to invoke KeyClicked is raised.");
            }

            return null;
        }

        void InvokeActionAndEvent(RemoteControlKeyTypes keyType, string keyName)
        {
            RemoteControlKeyEventArgs args = CreateArgs(keyType, keyName);
            if (args == null)
                return;

            IList<RemoteKeyHandler> handlers = new List<RemoteKeyHandler>();
            if (Element is Page)
            {
                if (Element == currentPage)
                    handlers = InputEvents.GetEventHandlers(Element);
            }
            else
            {
                handlers = InputEvents.GetEventHandlers(Element);
            }
            foreach (RemoteKeyHandler item in handlers)
            {
                item.SendKeyEvent(args);
            }
        }
    }
}
