using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElmSharp;
using Tizen.TV.UIControls.Forms.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportEffect(typeof(AccessKeyEffect), "AccessKeyEffect")]
namespace Tizen.TV.UIControls.Forms.Renderer
{
    public class AccessKeyEffect : PlatformEffect
    {
        RemoteControlKeyNames _targetKeyName;

        protected override void OnAttached()
        {
            try
            {
                EcoreKeyEvents.Instance.KeyDown += OnKeyDown;
                _targetKeyName = InputEvents.GetAccessKey(Element);
            }
            catch(Exception e)
            {
                Log.Error(UIControls.Tag, $"Failed to attach the effect : {e.Message}");
            }
        }

        protected override void OnDetached()
        {
            EcoreKeyEvents.Instance.KeyDown -= OnKeyDown;
        }

        Page GetParentPage()
        {
            var _targetElement = Element;

            if (_targetElement is Page page) return page;
            while (_targetElement != null && _targetElement.Parent != null)
            {
                if (_targetElement.Parent is Page parentPage) return parentPage;
                _targetElement = _targetElement.Parent;
            }
            return null;
        }

        void OnKeyDown(object sender, EcoreKeyEventArgs e)
        {
            var targetName = _targetKeyName.ToString();
            if (targetName == e.KeyName || targetName == "NUM" + e.KeyName)
            {
                var targetPage = GetParentPage();
                if(IsOnCurrentPage(Application.Current.MainPage, targetPage))
                {
                    ActiveOrFocusElement();
                }
            }
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

        void ActiveOrFocusElement()
        {
            if (Element is Xamarin.Forms.Button)
                (Element as Xamarin.Forms.Button).SendClicked();
            else
                (Element as VisualElement).Focus();
        }
    }
}
