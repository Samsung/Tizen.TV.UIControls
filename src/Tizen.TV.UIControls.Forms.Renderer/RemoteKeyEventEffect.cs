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
            //TODO: e.Flags = EvasEventFlag.OnHold; when Handled was true
            InvokeActionAndEvent(RemoteControlKeyTypes.KeyDown, e.KeyName);
        }

        void OnViewKeyUp(object sender, EvasKeyEventArgs e)
        {
            InvokeActionAndEvent(RemoteControlKeyTypes.KeyUp, e.KeyName);
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

        void InvokeActionAndEvent(RemoteControlKeyTypes keyType, string keyName)
        {
            RemoteControlKeyEventArgs args = CreateArgs(keyType, keyName);
            if (args == null)
                return;

            IList<RemoteKeyHandler> handlers = new List<RemoteKeyHandler>();
            if (Element is Page)
            {
                if (IsOnCurrentPage((Page)Element))
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

        bool IsOnCurrentPage(Page targetPage)
        {
            //TODO: Don't use Navigation
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            if (!(Element is IPageContainer<Page>))
            {
                while (currentPage is IPageContainer<Page>)
                {
                    currentPage = (currentPage as IPageContainer<Page>).CurrentPage;
                }
            }
            // TODO : Handle MasterDetailPage
            return currentPage == targetPage;
        }
    }
}
