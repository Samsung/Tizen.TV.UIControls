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
using System.Collections.Generic;

namespace Sample
{
    public class TestCategory
    {
        public string Name { get; set; }
        public Type PageType { get; set; }
    }

    public class MainPageModel
    {
        public List<TestCategory> TestCategories { get; }

        public MainPageModel()
        {
            TestCategories = new List<TestCategory>
            {
                new TestCategory
                {
                    Name = "Page Transition Test",
                    PageType = typeof(AnimatedNaviMainPage),
                },
                new TestCategory
                {
                    Name = "MediaPlayer Test",
                    PageType = typeof(PlayerMainPage),
                },
                new TestCategory
                {
                    Name = "RemoteCotrol Test",
                    PageType = typeof(RemoteControlMainPage),
                },
                new TestCategory
                {
                    Name = "RecycleItemsView Test",
                    PageType = typeof(RIVMainPage),
                },
                new TestCategory
                {
                    Name = "DrawerLayout Test",
                    PageType = typeof(DrawerLayout.DrawerMainPage),
                },
                new TestCategory
                {
                    Name = "Entry and Editor Test",
                    PageType = typeof(EntryMainPage),
                },
                new TestCategory
                {
                    Name = "Custom Focus Test",
                    PageType = typeof(Focus.FocusMainPage),
                },
                new TestCategory
                {
                    Name = "ContentButton Test",
                    PageType = typeof(ContentButton.ContentButtonTest)
                },
                new TestCategory
                {
                    Name = "ContentPopup Test",
                    PageType = typeof(ContentPopup.ContentPopupMainPage),
                }
            };
        }
    }
}
