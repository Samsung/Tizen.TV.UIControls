using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.DrawerLayout
{
    class TestModel
    {
        public string Name { get; set; }
        public Type PageType { get; set; }
    }

    class DrawerTestModel
    {
        public IList<TestModel> TestList { get; }

        public DrawerTestModel()
        {
            TestList = new List<TestModel>
            {
                new TestModel
                {
                    Name = "Basic Test",
                    PageType = typeof(DrawerBasicTest)
                },
                new TestModel
                {
                    Name = "Right Drawer Test",
                    PageType = typeof(RightDrawerTest)
                },
                new MenuListModel
                {
                    Name = "Drawer Sample1",
                    PageType = typeof(DrawerSample1),
                    Items = new List<MenuModel>
                    {
                        new MenuModel
                        {
                            Text = "Item1",
                            Path = "poster1.jpg"
                        },
                        new MenuModel
                        {
                            Text = "Item2",
                            Path = "poster2.jpg"
                        },
                        new MenuModel
                        {
                            Text = "Item3",
                            Path = "poster.jpg"
                        },
                        new MenuModel
                        {
                            Text = "Item4",
                            Path = "04 E.T. 2.jpeg"
                        },
                        new MenuModel
                        {
                            Text = "Item5",
                            Path = "05 Vertigo.jpg"
                        },
                        new MenuModel
                        {
                            Text = "Item6",
                            Path = "06 Alien.jpg"
                        }
                    }
                },
                new MenuListModel
                {
                    Name = "Drawer Sample2",
                    PageType = typeof(DrawerSample2),
                    Items = new List<MenuModel>
                    {
                        new MenuModel
                        {
                            Text = "Item1",
                            Path = "poster1.jpg"
                        },
                        new MenuModel
                        {
                            Text = "Item2",
                            Path = "poster2.jpg"
                        },
                        new MenuModel
                        {
                            Text = "Item3",
                            Path = "poster3.jpg"
                        },
                        new MenuModel
                        {
                            Text = "Item4",
                            Path = "poster4.jpeg"
                        },
                        new MenuModel
                        {
                            Text = "Item5",
                            Path = "poster5.jpg"
                        },
                        new MenuModel
                        {
                            Text = "Item6",
                            Path = "poster6.jpg"
                        }
                    }
                },
                new GroupListModel
                {
                    PageType = typeof(NestedDrawerPage),
                    Name = "Nested Drawer Test",
                    Items = new List<MenuListModel>()
                    {
                        new MenuListModel()
                        {
                            Text = "Volvo",
                            Items = new List<MenuModel>()
                            {
                                new MenuModel
                                {
                                    Text = "XC40",
                                    Path = "https://imgauto-phinf.pstatic.net/20180625_214/auto_1529906298071e62U0_PNG/20180625145753_EtYYVrAE.png?type=f508_367"
                                },
                                new MenuModel
                                {
                                    Text = "XC60",
                                    Path = "https://imgauto-phinf.pstatic.net/20171218_138/auto_1513567687086QU07L_PNG/20171218122805_CnAq2Tsy.png?type=f508_367"
                                },
                                new MenuModel
                                {
                                    Text = "XC90",
                                    Path = "https://imgauto-phinf.pstatic.net/20180329_122/auto_1522301514528jVzHj_PNG/20180329143106_Gz5jBCrM.png?type=f508_367"
                                },
                                new MenuModel
                                {
                                    Text = "S60",
                                    Path = "https://imgauto-phinf.pstatic.net/20171207_19/auto_15126251519833GUMg_PNG/20171207143846_gxXvO7xL.png?type=f508_367"
                                },
                                new MenuModel
                                {
                                    Text = "S90",
                                    Path = "https://imgauto-phinf.pstatic.net/20180615_136/auto_1529025830950WTqRj_PNG/20180615102349_0X1EL88r.png?type=f508_367"
                                }
                            }
                        },
                        new MenuListModel()
                        {
                            Text = "BMW",
                            Items = new List<MenuModel>()
                            {
                                new MenuModel
                                {
                                    Text = "X4",
                                    Path = "https://imgauto-phinf.pstatic.net/20180704_8/auto_1530694218796qmEHD_PNG/20180704175016_5hwolcQB.png?type=f318_230"
                                },
                                new MenuModel
                                {
                                    Text = "M5",
                                    Path = "https://imgauto-phinf.pstatic.net/20180509_272/auto_1525836479891APrjj_PNG/20180509122755_3yrBdfAY.png?type=f318_230"
                                },
                                new MenuModel
                                {
                                    Text = "i8",
                                    Path = "https://imgauto-phinf.pstatic.net/20171201_282/auto_1512103236495bkS20_PNG/20171201134033_omsj34uD.png?type=f318_230"
                                },
                                new MenuModel
                                {
                                    Text = "3 series",
                                    Path = "https://imgauto-phinf.pstatic.net/20180327_137/auto_1522130792216yfMIE_PNG/20180327150605_LuuarbCD.png?type=f318_230"
                                },
                                new MenuModel
                                {
                                    Text = "5 series",
                                    Path = "https://imgauto-phinf.pstatic.net/20170221_12/auto_1487648886927tgrXQ_PNG/20170221124804_32R34dE1.png?type=f318_230"
                                }
                            }
                        },
                        new MenuListModel()
                        {
                            Text = "Mercedes Benz",
                            Items = new List<MenuModel>()
                            {
                                new MenuModel
                                {
                                    Text = "CLS class",
                                    Path = "https://imgauto-phinf.pstatic.net/20180618_10/auto_1529313436137ybaH3_PNG/20180618181653_7DkZjgOu.png?type=f318_230"
                                },
                                new MenuModel
                                {
                                    Text = "E class",
                                    Path = "https://imgauto-phinf.pstatic.net/20180129_68/auto_1517215440716qxPF5_PNG/20180129174359_UC5Vk02v.png?type=f318_230"
                                },
                                new MenuModel
                                {
                                    Text = "C class",
                                    Path = "https://imgauto-phinf.pstatic.net/20180122_115/auto_1516585412618vlVNx_PNG/20180122104330_xqrVTjq9.png?type=f318_230"
                                },
                                new MenuModel
                                {
                                    Text = "GLA class",
                                    Path = "https://imgauto-phinf.pstatic.net/20170911_10/auto_15051031723480XF0K_PNG/20170911131215_Xc2yZy0Z.png?type=f508_367"
                                },
                                new MenuModel
                                {
                                    Text = "GLC class",
                                    Path = "https://imgauto-phinf.pstatic.net/20180614_103/auto_1528935527269uUcOA_PNG/20180614091759_vbEMKhL0.png?type=f318_230"
                                },
                                new MenuModel
                                {
                                    Text = "GLE class",
                                    Path = "https://imgauto-phinf.pstatic.net/20150326_30/auto_1427357465545KHMnJ_PNG/20150326171045_eXDTDL8B.png?type=f508_367"
                                }
                            }
                        },
                        new MenuListModel()
                        {
                            Text = "AUDI",
                            Items = new List<MenuModel>()
                            {
                                new MenuModel
                                {
                                    Text = "Q8",
                                    Path = "https://imgauto-phinf.pstatic.net/20180716_239/auto_1531714515906TYEJO_PNG/20180716131445_I0NkG36M.png?type=f318_230"
                                },
                                new MenuModel
                                {
                                    Text = "A4",
                                    Path = "https://imgauto-phinf.pstatic.net/20180702_193/auto_1530509505891rCQ3T_PNG/20180702142929_pMcCHy2W.png?type=f318_230"
                                },
                                new MenuModel
                                {
                                    Text = "A6",
                                    Path = "https://imgauto-phinf.pstatic.net/20180228_196/auto_1519781288205F331z_PNG/20180228102742_ExGWmx2R.png?type=f318_230"
                                },
                                new MenuModel
                                {
                                    Text = "A7",
                                    Path = "https://imgauto-phinf.pstatic.net/20171024_53/auto_15088269449621VDmK_PNG/20171024153543_Hj80UIrN.png?type=f318_230"
                                },
                                new MenuModel
                                {
                                    Text = "R8",
                                    Path = "https://imgauto-phinf.pstatic.net/20160603_69/auto_1464928277326VMF0g_PNG/20160603133056_U7Ifalun.png?type=f508_367"
                                },
                            }
                        },
                    }
                }
            };
        }
    }

    class MenuListModel : TestModel
    {
        public string Text { get; set; }
        public IList<MenuModel> Items { get; set; }
    }

    class GroupListModel : TestModel
    {
        public IList<MenuListModel> Items { get; set; }
    }

    class MenuModel
    {
        public string Text { get; set; }
        public string Path { get; set; }
    }
}
