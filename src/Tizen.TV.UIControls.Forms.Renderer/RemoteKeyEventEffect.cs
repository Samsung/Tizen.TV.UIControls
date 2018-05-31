using System;
using System.Collections.Generic;
using System.Text;
using ElmSharp;
using Tizen.TV.UIControls.Forms.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ResolutionGroupName("TizenTVUIControl")]
[assembly: ExportEffect(typeof(RemoteKeyEventEffect), "RemoteKeyEventEffect")]
namespace Tizen.TV.UIControls.Forms.Renderer
{
    public class RemoteKeyEventEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                if (Element is Page)
                {
                    EcoreKeyEvents.Instance.KeyDown += OnPageKeyDown;
                    EcoreKeyEvents.Instance.KeyUp += OnPageKeyUp;
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
                EcoreKeyEvents.Instance.KeyDown -= OnPageKeyDown;
                EcoreKeyEvents.Instance.KeyUp -= OnPageKeyUp;
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
            if (InvokeActionAndEvent(RemoteControlKeyTypes.KeyDown, e.KeyName))
                e.Flags = EvasEventFlag.OnHold;
        }

        void OnViewKeyUp(object sender, EvasKeyEventArgs e)
        {
            if (InvokeActionAndEvent(RemoteControlKeyTypes.KeyUp, e.KeyName))
                e.Flags = EvasEventFlag.OnHold;
        }

        static RemoteControlKeyEventArgs CreateArgs(RemoteControlKeyTypes keyType, string keyName)
        {
            RemoteControlKeyNames key = RemoteControlKeyNames.Undefined;
            if (Enum.TryParse(keyName, out key))
                return new RemoteControlKeyEventArgs(keyType, key);
            if (Enum.TryParse("NUM" + keyName, out key))
                return new RemoteControlKeyEventArgs(keyType, key);

            return new RemoteControlKeyEventArgs(keyType, key);
        }

        bool InvokeActionAndEvent(RemoteControlKeyTypes keyType, string keyName)
        {
            RemoteControlKeyEventArgs args = CreateArgs(keyType, keyName);
            if (args == null)
                return false;

            IList<RemoteKeyHandler> handlers = new List<RemoteKeyHandler>();
            if (Element is Page targetPage)
            {
                if (!IsOnCurrentPage(Application.Current.MainPage, targetPage))
                {
                    return false;
                }
            }

            handlers = InputEvents.GetEventHandlers(Element);
            foreach (RemoteKeyHandler item in handlers)
            {
                item.SendKeyEvent(args);
            }
            return args.Handled;
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
                if (masterDetailPage.IsPresented)
                {
                    if (IsOnCurrentPage(masterDetailPage.Master, targetPage))
                        return true;
                }
                if (!(masterDetailPage.MasterBehavior == MasterBehavior.Popover && masterDetailPage.IsPresented))
                {
                    if (IsOnCurrentPage(masterDetailPage.Detail, targetPage))
                        return true;
                }
            }

            return false;
        }
    }
}
