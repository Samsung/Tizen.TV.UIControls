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
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms.Renderer
{
    public class NavigationStack : Box
    {
        public IReadOnlyList<EvasObject> Stack => InternalStack;

        public bool IsPreviousViewVisible { get; set; }

        List<EvasObject> InternalStack { get; set; }

        EvasObject CurrentView { get; set; }

        EvasObject PreviousView => InternalStack.Count > 1 ? InternalStack[InternalStack.Count - 2] : null;

        public NavigationStack(EvasObject parent) : base(parent)
        {
            InternalStack = new List<EvasObject>();
            SetLayoutCallback(OnLayout);
        }

        public void Push(EvasObject view, bool isAnimated = false)
        {
            InternalStack.Add(view);
            PackEnd(view);

            if (isAnimated)
            {
                if (CurrentView != null)
                {
                    CurrentView.AllEventsFrozen = true;
                }
            }
            else
            {
                UpdateTopView();
            }
        }

        public void SendPushAnimationFinished()
        {
            if (PreviousView != null)
            {
                PreviousView.AllEventsFrozen = false;
            }
            UpdateTopView();
        }

        public void Pop()
        {
            if (CurrentView != null)
            {
                var tobeRemoved = CurrentView;
                InternalStack.Remove(tobeRemoved);
                UnPack(tobeRemoved);
                UpdateTopView();
                // if Pop was called by removed page,
                // Unrealize cause deletation of NativeCallback, it could be a cause of crash
                Device.BeginInvokeOnMainThread(() =>
                {
                    tobeRemoved.Unrealize();
                });
            }
        }

        public void PopToRoot()
        {
            while (InternalStack.Count > 1)
            {
                Pop();
            }
        }

        public void Insert(EvasObject before, EvasObject view)
        {
            view.Hide();
            var idx = InternalStack.IndexOf(before);
            InternalStack.Insert(idx, view);
            PackEnd(view);
            UpdateTopView();
        }

        public void Remove(EvasObject view)
        {
            InternalStack.Remove(view);
            UnPack(view);
            UpdateTopView();
            Device.BeginInvokeOnMainThread(() =>
            {
                view?.Unrealize();
            });
        }

        public void ShowPreviousView()
        {
            PreviousView?.Show();
            CurrentView.AllEventsFrozen = true;
        }

        void UpdateTopView()
        {
            if (CurrentView != InternalStack.LastOrDefault())
            {
                if (!IsPreviousViewVisible)
                    CurrentView?.Hide();
                CurrentView = InternalStack.LastOrDefault();
                CurrentView?.Show();
                (CurrentView as Widget)?.SetFocus(true);
            }
        }

        void OnLayout()
        {
            foreach (var view in Stack)
            {
                view.Geometry = Geometry;
            }
        }
    }
}
