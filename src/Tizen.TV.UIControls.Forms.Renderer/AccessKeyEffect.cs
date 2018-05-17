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
                return page;
            }
        }

        protected override void OnAttached()
        {
            try
            {
                _ecoreKeyEvents.KeyDown += OnkeyDown;
                _targetKeyName = InputEvents.GetAccessKey(Element);
            }
            catch(Exception e)
            {
                Console.WriteLine("Failed to attach the effect : " + e);
            }
        }

        protected override void OnDetached()
        {
            _ecoreKeyEvents.KeyDown -= OnkeyDown;
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

        void OnkeyDown(object sender, EcoreKeyEventArgs e)
        {
            string _keyName = "";
            if (int.TryParse(e.KeyName, out int result))
            {
                if (result >= 0 && result <= 9)
                    _keyName = String.Concat("NUM", e.KeyName);
            }
            else
                _keyName = e.KeyName;

            if (_keyName.Equals(_targetKeyName.ToString()))
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
