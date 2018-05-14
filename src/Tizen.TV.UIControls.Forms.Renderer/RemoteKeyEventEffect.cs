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
        static int _ecoreEventsCounter = 0;

        // TODO: Test Multipage
        Page currentPage
        {
            get
            {
                var page = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
                while (page is NavigationPage)
                {
                    page = (page as NavigationPage).CurrentPage;
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
                    if (_ecoreEventsCounter == 0)
                    {
                        _ecoreKeyEvents.KeyDown += Page_keyDown;
                        _ecoreKeyEvents.KeyUp += Page_keyUp;
                    }
                    _ecoreEventsCounter++;
                }
                else
                {
                    Control.KeyDown += Control_KeyDown;
                    Control.KeyUp += Control_KeyUp;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception : " + e);
            }
        }

        protected override void OnDetached()
        {
            if (Element is Page)
            {
                _ecoreEventsCounter--;
                if (_ecoreEventsCounter == 0)
                {
                    _ecoreKeyEvents.KeyDown -= Page_keyDown;
                    _ecoreKeyEvents.KeyUp -= Page_keyUp;
                }
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
            IList<RemoteKeyHandler> handlers;
            if (Element is Page)
            {
                handlers = InputEvents.GetEventHandlers(currentPage);
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
