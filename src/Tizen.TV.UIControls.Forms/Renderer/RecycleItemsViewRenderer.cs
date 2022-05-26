/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using ElmSharp;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Tizen;
using Microsoft.Maui.Controls.Platform;
using Tizen.Theme.Common.Renderer;
using Tizen.TV.UIControls.Forms;
using MButton = Microsoft.Maui.Controls.Button;
using MForms = Microsoft.Maui.Controls.Compatibility.Forms;

//[assembly: ExportRenderer(typeof(RecycleItemsView), typeof(RecycleItemsViewRenderer))]
//[assembly: ExportRenderer(typeof(RecycleItemsView.ContentLayout), typeof(RecycleItemsViewContentRenderer))]
//[assembly: ExportRenderer(typeof(MButton), typeof(PropagatableButtonRenderer))]
namespace Tizen.TV.UIControls.Forms
{
    class RecycleItemsViewContentRenderer : ViewRenderer<RecycleItemsView.ContentLayout, LayoutCanvas>
    {
        IRecycleItemsViewController ViewController => Element as IRecycleItemsViewController;

        protected override void OnElementChanged(ElementChangedEventArgs<RecycleItemsView.ContentLayout> e)
        {
            if (null == Control)
            {
                SetNativeControl(new LayoutCanvas(MForms.NativeParent));
                Control.AllowFocus(true);
                Control.KeyDown += OnKeyDown;
            }

            base.OnElementChanged(e);
        }

        void OnKeyDown(object sender, EvasKeyEventArgs e)
        {
            if (e.KeyName == "Left" || e.KeyName == "Right" || e.KeyName == "Up" || e.KeyName == "Down" || e.KeyName == "Return")
            {
                if (ViewController?.SendKeyDown(e.KeyName) ?? false)
                {
                    e.Flags = EvasEventFlag.OnHold;
                }
            }
        }
    }

    class RecycleItemsViewRenderer : LayoutRenderer
    {
        IRecycleItemsViewController ViewController => Element as IRecycleItemsViewController;
        public RecycleItemsViewRenderer()
        {
            RegisterPropertyHandler("FocusedView", UpdateFocusedView);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Microsoft.Maui.Controls.Compatibility.Layout> e)
        {
            base.OnElementChanged(e);
            Control.AllowFocus(true);
        }

        void UpdateFocusedView(bool init)
        {
            if (init)
                return;

            Platform.GetRenderer(ViewController.FocusedView)?.NativeView?.RaiseTop();
        }
    }


    class PropagatableButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<MButton> e)
        {
            base.OnElementChanged(e);
            // It is temporary workaround code
            // `PropagateEvents = true` is default value of EFL, but by mistake of Xamarin.Forms.Platform.Tizen implementation it was set to false
            // It was fixed in latest version but not released so we fixed with workaround code
            Control.PropagateEvents = true;
        }
    }


}
