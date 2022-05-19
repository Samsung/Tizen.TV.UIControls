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

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Tizen;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Controls.Platform;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Tizen.Theme.Common;
using Tizen.Theme.Common.Renderer;

[assembly: Microsoft.Maui.Controls.Compatibility.ExportRenderer(typeof(AnimatedNavigationPage), typeof(AnimatedNavigationPageRenderer))]
namespace Tizen.Theme.Common.Renderer
{
    public class AnimatedNavigationPageRenderer : VisualElementRenderer<AnimatedNavigationPage>
    {
        NavigationStack _navigationStack;
        TaskCompletionSource<bool> _currentTaskSource = null;
        Page _previousPage = null;
        Page CurrentPage => Element.CurrentPage;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_navigationStack != null)
                {
                    _navigationStack.Unrealize();
                    _navigationStack = null;
                }
            }
            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AnimatedNavigationPage> e)
        {
            if (_navigationStack == null)
            {
                _navigationStack = new NavigationStack(Forms.NativeParent);
                _navigationStack.Show();
                _navigationStack.SetAlignment(-1, -1);
                _navigationStack.SetWeight(1, 1);
                _navigationStack.Show();
                SetNativeView(_navigationStack);
            }

            if (e.OldElement != null)
            {
                var navigation = e.OldElement as INavigationPageController;
                navigation.PopRequested -= OnPopRequested;
                navigation.PopToRootRequested -= OnPopToRootRequested;
                navigation.PushRequested -= OnPushRequested;
                navigation.RemovePageRequested -= OnRemovePageRequested;
                navigation.InsertPageBeforeRequested -= OnInsertPageBeforeRequested;
            }

            if (e.NewElement != null)
            {
                var navigation = e.NewElement as INavigationPageController;
                navigation.PopRequested += OnPopRequested;
                navigation.PopToRootRequested += OnPopToRootRequested;
                navigation.PushRequested += OnPushRequested;
                navigation.RemovePageRequested += OnRemovePageRequested;
                navigation.InsertPageBeforeRequested += OnInsertPageBeforeRequested;
                _previousPage = e.NewElement.CurrentPage;
                _navigationStack.IsPreviousViewVisible = Element.IsPreviousPageVisible;
            }
            base.OnElementChanged(e);
        }

        protected override void OnElementReady()
        {
            base.OnElementReady();
            var pageController = Element as IPageController;

            foreach (Page page in pageController.InternalChildren)
            {
                _navigationStack.Push(Platform.GetOrCreateRenderer(page).NativeView);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == NavigationPage.CurrentPageProperty.PropertyName)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    (_previousPage as IPageController)?.SendDisappearing();
                    _previousPage = Element.CurrentPage;
                    (_previousPage as IPageController)?.SendAppearing();
                });
            }
            else if (e.PropertyName == AnimatedNavigationPage.IsPreviousPageVisibleProperty.PropertyName)
            {
                _navigationStack.IsPreviousViewVisible = Element.IsPreviousPageVisible;
            }
        }

        void OnPushRequested(object sender, NavigationRequestedEventArgs nre)
        {
            var renderer = Platform.GetOrCreateRenderer(nre.Page);
            var animation = CurrentPage.GetPushAnimation();
            var animated = nre.Animated && animation != null;

            _navigationStack.Push(renderer.NativeView, animated);
            if (animated)
            {
                animation.GetCallback().Invoke(0.0001);
                var rate = CurrentPage.GetPushAnimationRate();
                var length = CurrentPage.GetPushAnimationLength();
                animation.Commit(CurrentPage, "PushAnimation", rate, length, null,
                    finished: (v, c) =>
                    {
                        _navigationStack.SendPushAnimationFinished();
                        CompleteCurrentNavigationTask();
                    });
                _currentTaskSource = new TaskCompletionSource<bool>();
                nre.Task = _currentTaskSource.Task;
            }
            else
            {
                nre.Task = Task.FromResult(true);
            }
        }

        void OnPopRequested(object sender, NavigationRequestedEventArgs nre)
        {
            if ((Element as IPageController).InternalChildren.Count == _navigationStack.Stack.Count)
            {
                nre.Page?.SendDisappearing();

                var animation = CurrentPage.GetPopAnimation();

                if (nre.Animated && animation != null)
                {
                    var rate = CurrentPage.GetPopAnimationRate();
                    var length = CurrentPage.GetPopAnimationLength();
                    _navigationStack.ShowPreviousView();
                    animation.Commit(CurrentPage, "PopAnimation", rate, length, null,
                        finished: (v, c) =>
                        {
                            _navigationStack.Pop();
                            CompleteCurrentNavigationTask();
                        });


                    _currentTaskSource = new TaskCompletionSource<bool>();
                    nre.Task = _currentTaskSource.Task;
                }
                else
                {
                    _navigationStack.Pop();
                    nre.Task = Task.FromResult(true);
                }
            }
        }

        void OnPopToRootRequested(object sender, NavigationRequestedEventArgs nre)
        {
            _navigationStack.PopToRoot();
            nre.Task = Task.FromResult(true);

        }

        void OnRemovePageRequested(object sender, NavigationRequestedEventArgs nre)
        {
            var renderer = Platform.GetRenderer(nre.Page);
            if (renderer == null)
            {
                nre.Task = Task.FromException<bool>(new ArgumentException("Can't found page on stack", nameof(nre.Page)));
                return;
            }
            _navigationStack.Remove(renderer.NativeView);
            nre.Task = Task.FromResult(true);
        }

        void OnInsertPageBeforeRequested(object sender, NavigationRequestedEventArgs nre)
        {
            if (nre.BeforePage == null)
                throw new ArgumentException("BeforePage is null");
            if (nre.Page == null)
                throw new ArgumentException("Page is null");

            var before = Platform.GetRenderer(nre.BeforePage).NativeView ?? null;
            if (before == null)
            {
                nre.Task = Task.FromException<bool>(new ArgumentException("Can't found page on stack", nameof(nre.BeforePage)));
                return;
            }
            var renderer = Platform.GetOrCreateRenderer(nre.Page);
            _navigationStack.Insert(before, renderer.NativeView);
            nre.Task = Task.FromResult(true);
        }

        void CompleteCurrentNavigationTask()
        {
            if (_currentTaskSource != null)
            {
                var tmp = _currentTaskSource;
                _currentTaskSource = null;
                tmp.SetResult(true);
            }
        }
    }
}
