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
using CPopup = Tizen.Theme.Common.ContentPopup;

namespace Sample.ContentPopup
{
    class TestModel
    {
        public string Name { get; set; }
        public Type PageType { get; set; }
    }

    class ContentPopupTestModel
    {
        public IList<TestModel> TestList { get; }
        public ContentPopupTestModel()
        {
            TestList = new List<TestModel>
            {
                new TestModel
                {
                    Name = "Basic Test",
                    PageType = typeof(ContentPopupBasicTest)
                },
                new TestModel
                {
                    Name = "Background Color Test",
                    PageType = typeof(ContentPopupBGColorTest)
                },
            };
        }
    }
    public class MyPopup : CPopup
    {
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
