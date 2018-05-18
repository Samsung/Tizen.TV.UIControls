using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElmSharp;
using Tizen.TV.UIControls.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportEffect(typeof(AccessKeyEffect), "AccessKeyEffect")]
namespace Tizen.TV.UIControls.Forms
{
    class AccessKeyEffect : PlatformEffect
    {
        EcoreKeyEvents _ecoreKeyEvents = EcoreKeyEvents.Instance;
        RemoteControlKeyNames _targetKeyName;

        protected override void OnAttached()
        {
            try
            {
                _ecoreKeyEvents.KeyDown += OnKeyDown;
                _targetKeyName = InputEvents.GetAccessKey(Element);
            }
            catch(Exception e)
            {
                Console.WriteLine("Failed to attach the effect : " + e);
            }
        }

        protected override void OnDetached()
        {
            _ecoreKeyEvents.KeyDown -= OnKeyDown;
        }

        Page GetParentPage()
        {
            var _targetElement = Element;
            if (_targetElement != null)
            {
                if (_targetElement is Page)
                    return (_targetElement as Page);
                var parent = _targetElement.Parent;
                while (parent != null)
                {
                    if (parent is Page)
                    {
                        return (Page)parent;
                    }
                    parent = parent.Parent;
                }
            }
            return null;
        }

        void OnKeyDown(object sender, EcoreKeyEventArgs e)
        {
            string keyName = "";
            if (int.TryParse(e.KeyName, out int result))
            {
                if (result >= 0 && result <= 9)
                    keyName = String.Concat("NUM", e.KeyName);
            }
            else
                keyName = e.KeyName;

            if (keyName.Equals(_targetKeyName.ToString()))
            {
                var targetPage = GetParentPage();
                if(IsOnCurrentPage(targetPage))
                {
                    ActiveOrFocusElement();
                }
            }
        }

        bool IsOnCurrentPage(Page targetPage)
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            if (!(Element is IPageContainer<Page>))
            {
                while (currentPage is IPageContainer<Page>)
                {
                    currentPage = (currentPage as IPageContainer<Page>).CurrentPage;
                }
            }
            return currentPage == targetPage;
        }

        void ActiveOrFocusElement()
        {
            if (Element is Xamarin.Forms.Button)
                (Element as Xamarin.Forms.Button).SendClicked();
            else
                (Element as VisualElement).Focus();
        }
    }
}
