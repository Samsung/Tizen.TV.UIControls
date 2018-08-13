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

using System;
using Xamarin.Forms;
using Tizen.TV.UIControls.Forms;

namespace Sample
{
    public class TestEntry : ContentPage
    {
        int _clickedTimes = 0;
        public TestEntry()
        {
            Label statusLabel = new Label();
            var entry = new Entry
            {
                Text = "Test Entry"
            };
            entry.TextChanged += (s, e) =>
            {
                statusLabel.Text = $"Entry TextChanged: {e.NewTextValue}";
            };
            entry.Completed += (s, e) =>
            {
                statusLabel.Text = $"Entry Completed: {entry.Text}";
            };
            var editor = new Editor
            {
                Text = "Test Editor"
            };
            editor.TextChanged += (s, e) =>
            {
                statusLabel.Text = $"Editor TextChanged: {e.NewTextValue}";
            };
            editor.Completed += (s, e) =>
            {
                statusLabel.Text = $"Editor Completed: {entry.Text}";
            };

            Content = new StackLayout
            {
                Children =
                {
                    entry,
                    editor,
                    statusLabel,
                }
            };
        }
    }
}
