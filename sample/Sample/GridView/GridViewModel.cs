﻿/*
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
using System.Collections;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Sample.GridView
{
    class TestModel
    {
        public string Name { get; set; }
        public Type PageType { get; set; }
    }

    class GridViewModel
    {
        public IList<TestModel> TestList { get; }

        public GridViewModel()
        {
            TestList = new List<TestModel>
            {
                new ItemsModel
                {
                    Name = "DataTemplate Test",
                    PageType = typeof(DataTemplateTest),
                    Items = PosterModel.MakeAlbumArtsModel()
                },
                new ItemsModel
                {
                    Name = "DataTemplate Test2",
                    PageType = typeof(DataTemplateTest2),
                    Items = PosterModel.MakeAlbumArtsModel()
                },
                new ItemsModel
                {
                    Name = "Vertical Test",
                    PageType = typeof(VerticalTest),
                    Items = ColorModel.MakeModel(30)
                },
                new ItemsModel
                {
                    Name = "Horizontal Test",
                    PageType = typeof(HorizontalTest),
                    Items = ColorModel.MakeModel(30)
                },
            };
        }
    }

    class ItemsModel : TestModel
    {
        public IList Items { get; set; }
    }

    class DoubleItemsModel : ItemsModel
    {
        public IList Items2 { get; set; }
    }

    class ColorModel
    {
        public Color Color { get; set; }
        public string Text { get; set; }

        public static List<ColorModel> MakeModel(int count = 3000)
        {
            List<ColorModel> list = new List<ColorModel>();
            Random rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                Color color = Color.FromRgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
                list.Add(new ColorModel
                {
                    Color = color,
                    Text = $"Color: {(int)(color.Red*255)}, {(int)(color.Green * 255)}, {(int)(color.Blue * 255)}"
                });
            }
            return list;
        }
    }


    class PosterModel
    {
        public ImageSource Source { get; set; }
        public string Text { get; set; }
        public string DetailText { get; set; }

        public static List<PosterModel> MakeModel(bool shortdetail=false)
        {
            List<string> posters = new List<string>();
            for(int i = 1; i <= 50; i++)
            {
                posters.Add($"poster{i}.jpg");
            }
            List<PosterModel> items = new List<PosterModel>();
            foreach (var i in posters)
            {
                var texts = i.Split('.');
                texts[texts.Length - 1] = "";
                items.Add(new PosterModel
                {
                    Source = ImageSource.FromFile("" + i),
                    Text = string.Join(" ", texts),
                    DetailText = shortdetail ? "A great example" : "A great example of colour scheme that extends from a film to its marketing. Yellow emanates from this heartwarming Sundance hit, seen on Paul Dano’s t-shirt and the lovably rubbish VW campervan, here flooding the negative space of both trailer and poster.",
                });
            }
            return items;
        }

        public static List<PosterModel> MakeAlbumArtsModel()
        {
            List<string> albums = new List<string>(0);
            for(int i = 1; i <= 14; i++)
            {
                albums.Add($"album{i}.jpg");
            }
            List<string> description = new List<string>
            {
                "1984, Van Halen.jpg",
                "1989, Taylor Swift.jpg",
                "Abbey Road, The Beatles.jpg",
                "American IV - The Man Comes Around, Johnny Cash.jpg",
                "Back in Black, ACDC.jpg",
                "Elvis Presley, Elvis Presley.jpg",
                "Feel So Close, Calvin Harris.jpg",
                "Feel, Third Party vs Cicada.jpg",
                "I'm in Love (I Wanna Do It), Alex Gaudino.jpg",
                "If I Lose Myself, OneRepublic vs Alesso.jpg",
                "Nevermind, Nirvana.jpg",
                "Nothing's Real, Shura.jpg",
                "Runaway (U&I), Galantis.jpg",
                "The Freewheelin' Bob Dylan, Bob Dylan.jpg",
            };
            List<PosterModel> items = new List<PosterModel>();

            int n = 0;

            foreach (var i in albums)
            {
                var texts = description[n].Split(',');
                n++;
                items.Add(new PosterModel
                {
                    Source = ImageSource.FromFile("" + i),
                    Text = texts[0],
                    DetailText = texts[1]
                });
            }
            return items;
        }
    }
}