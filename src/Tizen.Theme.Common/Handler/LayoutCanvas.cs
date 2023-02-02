﻿///*
// * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
// *
// * Licensed under the Apache License, Version 2.0 (the License);
// * you may not use this file except in compliance with the License.
// * You may obtain a copy of the License at
// *
// * http://www.apache.org/licenses/LICENSE-2.0
// *
// * Unless required by applicable law or agreed to in writing, software
// * distributed under the License is distributed on an AS IS BASIS,
// * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// * See the License for the specific language governing permissions and
// * limitations under the License.
// */

//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Collections.Specialized;
////using Microsoft.Maui.Controls.Compatibility.Platform.Tizen.Native;
////using ElmSharp;
//using TView = Tizen.NUI.BaseComponents.View;
//using ViewGroup = Tizen.UIExtensions.NUI.ViewGroup;
//using TLayoutEventArgs = Tizen.UIExtensions.Common.LayoutEventArgs;
//using Tizen.UIExtensions.Common;

//namespace Tizen.Theme.Common.Renderer
//{
//    public class LayoutCanvas : ViewGroup, IContainable<TView>
//    {
//        /// <summary>
//        /// The list of Views.
//        /// </summary>
//        readonly ObservableCollection<TView> _children = new ObservableCollection<TView>();
//        ViewGroup _viewGroup;


//        /// <summary>
//        /// Initializes a new instance of the <see cref="Xamarin.Forms.Platform.Tizen.Native.Canvas"/> class.
//        /// </summary>
//        /// <remarks>Canvas doesn't support replacing its children, this will be ignored.</remarks>
//        /// <param name="parent">Parent of this instance.</param>
//        public LayoutCanvas(TView parent) : base()
//        {
//            //SetTheme("layout", "elm_widget", "default");
//            _viewGroup = new ViewGroup();
//            //SetContent(_viewGroup);

//            _children.CollectionChanged += (o, e) =>
//            {
//                if (e.Action == NotifyCollectionChangedAction.Add)
//                {
//                    foreach (var v in e.NewItems)
//                    {
//                        var view = v as TView;
//                        if (null != view)
//                        {
//                            OnAdd(view);
//                        }
//                    }
//                }
//                else if (e.Action == NotifyCollectionChangedAction.Remove)
//                {
//                    foreach (var v in e.OldItems)
//                    {
//                        if (v is TView view)
//                        {
//                            OnRemove(view);
//                        }
//                    }
//                }
//                else if (e.Action == NotifyCollectionChangedAction.Reset)
//                {
//                    OnRemoveAll();
//                }
//            };
//        }

//        public event EventHandler<TLayoutEventArgs> LayoutUpdated
//        {
//            add { _viewGroup.LayoutUpdated += value; }
//            remove { _viewGroup.LayoutUpdated -= value; }
//        }

//        /// <summary>
//        /// Gets list of native elements that are placed in the canvas.
//        /// </summary>
//        public new IList<TView> Children
//        {
//            get
//            {
//                return _children;
//            }
//        }

//        ///// <summary>
//        ///// Provides destruction for native element and contained elements.
//        ///// </summary>
//        //protected override void OnUnrealize()
//        //{
//        //    //foreach (var child in _children)
//        //    //{
//        //    //    child.UnrealizeView();
//        //    //}

//        //    base.OnUnrealize();
//        //}

//        /// <summary>
//        /// Adds a new child to a container.
//        /// </summary>
//        /// <param name="view">Native element which will be added</param>
//        void OnAdd(TView view)
//        {
//            _viewGroup.Add(view);
//        }

//        /// <summary>
//        /// Removes a child from a container.
//        /// </summary>
//        /// <param name="view">Child element to be removed from canvas</param>
//        void OnRemove(TView view)
//        {
//            _viewGroup.Remove(view);
//        }

//        /// <summary>
//        /// Removes all children from a canvas.
//        /// </summary>
//        void OnRemoveAll()
//        {
//            //_viewGroup.UnPackAll();
//        }
//    }
//}