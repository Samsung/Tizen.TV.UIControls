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
        RemoteControlKeyNames _keyName;

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
                _ecoreKeyEvents.KeyDown += Page_keyDown;
                _keyName = InputEvents.GetAccessKey(Element);
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception : " + e);
            }
        }

        protected override void OnDetached()
        {
            _ecoreKeyEvents.KeyDown -= Page_keyDown;
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

        void Page_keyDown(object sender, EcoreKeyEventArgs e)
        {
            if (e.KeyName.Equals(_keyName.ToString()))
            {
                var targetPage = GetParentPage();
                if (currentPage == targetPage)
                {
                    SetFocusToElement();
                }
            }
        }

        void SetFocusToElement()
        {
            if (Element is Xamarin.Forms.Button)
                (Element as Xamarin.Forms.Button).SendClicked();
            else
                (Element as VisualElement).Focus();
        }
    }
}
