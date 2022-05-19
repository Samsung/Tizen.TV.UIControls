/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Microsoft.Maui.Controls;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Tizen.Theme.Common
{
    /// <summary>
    /// The ContentButton is a Button, which allows you to customize the View to be displayed.
    /// </summary>
    public class ContentButton : ContentView, IButtonController
    {
        const string PressedVisualState = "Pressed";

        /// <summary>
        /// BindableProperty. Identifies the Command bindable property.
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ContentButton), null, 
            propertyChanging: OnCommandChanging, propertyChanged: OnCommandChanged);

        /// <summary>
        /// BindableProperty. Identifies the CommandParameter bindable property.
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ContentButton), null, 
            propertyChanged: (bindable, oldvalue, newvalue) => CommandCanExcuteChanged(bindable, EventArgs.Empty));

        internal static readonly BindablePropertyKey IsPressedPropertyKey = BindableProperty.CreateReadOnly(nameof(IsPressed), typeof(bool), typeof(Button), default(bool));

        /// <summary>
        /// BindableProperty. Identifies the IsPressed bindable property.
        /// </summary>
        public static readonly BindableProperty IsPressedProperty = IsPressedPropertyKey.BindableProperty;

        /// <summary>
        ///  Gets a value that indicates whether this element is pressed or not.
        /// </summary>
        public bool IsPressed => (bool)GetValue(IsPressedProperty);

        /// <summary>
        /// Gets or sets command that is executed when the button is clicked.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Gets or sets command paramter that is executed when the button is clicked.
        /// </summary>
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// Occurs when the button is clicked.
        /// </summary>
        public event EventHandler Clicked;

        /// <summary>
        /// Occurs when the button is pressed.
        /// </summary>
        public event EventHandler Pressed;

        /// <summary>
        /// Occurs when the button is released.
        /// </summary>
        public event EventHandler Released;

        bool IsEnabledCore
        {
            set => SetValueCore(IsEnabledProperty, value);
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendClicked()
        {
            if (IsEnabled)
            {
                Command?.Execute(CommandParameter);
                Clicked?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendPressed()
        {
            if (IsEnabled)
            {
                SetIsPressed(true);
                ChangeVisualState();
                Pressed?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendReleased()
        {
            if (IsEnabled)
            {
                SetIsPressed(false);
                ChangeVisualState();
                Released?.Invoke(this, EventArgs.Empty);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        void SetIsPressed(bool isPressed) => SetValue(IsPressedPropertyKey, isPressed);

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            View content = Content;
            if (content != null)
            {
                SetInheritedBindingContext(content, BindingContext);
            }
        }

        static void OnCommandChanged(BindableObject bindable, object oldCommand, object newCommand)
        {
            ContentButton button = (ContentButton)bindable;
            if (newCommand is ICommand command)
            {
                command.CanExecuteChanged += button.OnCommandCanExecuteChanged;
            }
            CommandChanged(button);
        }

        static void CommandChanged(ContentButton button)
        {
            if (button.Command != null)
            {
                CommandCanExcuteChanged(button, EventArgs.Empty);
            }
            else
            {
                button.IsEnabledCore = true;
            }
        }

        static void OnCommandChanging(BindableObject bindable, object oldCommand, object newCommand)
        {
            ContentButton button = (ContentButton)bindable;
            if (oldCommand != null)
            {
                (oldCommand as ICommand).CanExecuteChanged -= button.OnCommandCanExecuteChanged;
            }
        }

        static void CommandCanExcuteChanged(object sender, EventArgs e)
        {
            var button = (ContentButton)sender;
            if (button.Command != null)
            {
                button.IsEnabledCore = button.Command.CanExecute(button.CommandParameter);
            }
        }

        void OnCommandCanExecuteChanged(object sender, EventArgs e)
        {
            ContentButton button = (ContentButton)sender;
            if (button.Command != null)
            {
                button.IsEnabledCore = button.Command.CanExecute(button.CommandParameter);
            }
        }

        protected override void ChangeVisualState()
        {
            if (IsEnabled && IsPressed)
            {
                VisualStateManager.GoToState(this, PressedVisualState);
            }
            else
            {
                base.ChangeVisualState();
            }
        }
    }
}
