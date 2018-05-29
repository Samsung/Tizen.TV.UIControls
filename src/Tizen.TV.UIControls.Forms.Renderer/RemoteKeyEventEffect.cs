using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElmSharp;
using Tizen.TV.UIControls.Forms.Impl;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ResolutionGroupName("TizenTVUIControl")]
[assembly: ExportEffect(typeof(RemoteKeyEventEffect), "RemoteKeyEventEffect")]
namespace Tizen.TV.UIControls.Forms.Impl
{
    class RemoteKeyEventEffect : PlatformEffect
    {
        EcoreKeyEvents _ecoreKeyEvents = EcoreKeyEvents.Instance;

        protected override void OnAttached()
        {
            try
            {
                if (Element is Page)
                {
                    _ecoreKeyEvents.KeyDown += OnPageKeyDown;
                    _ecoreKeyEvents.KeyUp += OnPageKeyUp;
                }
                else
                {
                    Control.KeyDown += OnViewKeyDown;
                    Control.KeyUp += OnViewKeyUp;
                }
            }
            catch (Exception e)
            {
                Log.Error(UIControls.Tag, $"Failed to attach the effect : {e.Message}");
            }
        }

        protected override void OnDetached()
        {
            if (Element is Page)
            {
                _ecoreKeyEvents.KeyDown -= OnPageKeyDown;
                _ecoreKeyEvents.KeyUp -= OnPageKeyUp;
            }
            else
            {
                Control.KeyDown -= OnViewKeyDown;
                Control.KeyUp -= OnViewKeyUp;
            }
        }

        void OnPageKeyDown(object sender, EcoreKeyEventArgs e)
        {
            InvokeActionAndEvent(RemoteControlKeyTypes.KeyDown, e.KeyName);
        }

        void OnPageKeyUp(object sender, EcoreKeyEventArgs e)
        {
            InvokeActionAndEvent(RemoteControlKeyTypes.KeyUp, e.KeyName);
        }

        void OnViewKeyDown(object sender, EvasKeyEventArgs e)
        {
            var handled = InvokeActionAndEvent(RemoteControlKeyTypes.KeyDown, e.KeyName);
            if (handled)
                e.Flags = EvasEventFlag.OnHold;
        }

        void OnViewKeyUp(object sender, EvasKeyEventArgs e)
        {
            var handled = InvokeActionAndEvent(RemoteControlKeyTypes.KeyUp, e.KeyName);
            if (handled)
                e.Flags = EvasEventFlag.OnHold;
        }

        static RemoteControlKeyEventArgs CreateArgs(RemoteControlKeyTypes keyType, string keyName)
        {
            if (int.TryParse(keyName, out int result))
            {
                if (result >= 0 && result <= 9)
                    keyName = keyName.Insert(0, "NUM");
            }
            try
            {
                RemoteControlKeyNames key = (RemoteControlKeyNames)Enum.Parse(typeof(RemoteControlKeyNames), keyName);
                return new RemoteControlKeyEventArgs(keyType, key);
            }
            catch
            {
                Console.WriteLine("Unkown Key event to invoke KeyClicked is raised.");
            }

            return null;
        }

        bool InvokeActionAndEvent(RemoteControlKeyTypes keyType, string keyName)
        {
            RemoteControlKeyEventArgs args = CreateArgs(keyType, keyName);
            if (args == null)
                return false;

            IList<RemoteKeyHandler> handlers = new List<RemoteKeyHandler>();
            if (Element is Page)
            {
                if (IsOnCurrentPage(Application.Current.MainPage, (Page)Element))
                {
                    handlers = InputEvents.GetEventHandlers(Element);
                }
            }
            else
            {
                handlers = InputEvents.GetEventHandlers(Element);
            }
            foreach (RemoteKeyHandler item in handlers)
            {
                item.SendKeyEvent(args);
            }
            if (args.Handled)
                return true;
            return false;
        }

        bool IsOnCurrentPage(Page currentPage, Page targetPage)
        {
            if (currentPage == targetPage)
                return true;

            while (currentPage is IPageContainer<Page>)
            {
                currentPage = (currentPage as IPageContainer<Page>).CurrentPage;
                if (currentPage == targetPage)
                    return true;
            }

            if (currentPage is MasterDetailPage masterDetailPage)
            {
                if (masterDetailPage.IsPresented == true)
                {
                    if (IsOnCurrentPage(masterDetailPage.Master, targetPage))
                        return true;
                }
                if (!(masterDetailPage.MasterBehavior == MasterBehavior.Popover && masterDetailPage.IsPresented == true))
                {
                    if (IsOnCurrentPage(masterDetailPage.Detail, targetPage))
                        return true;
                }
            }

            return false;
        }
    }
}
