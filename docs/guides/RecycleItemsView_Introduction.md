# RecycleItemsView
## Introduction
 RecycleItemsView is a view that wish to take in a list of user objects and produce views for each of them to be displayed. Especially, it can be used when the data is displayed in the same view template. and it reuse the templated view when it out of sight

## Concept
![concept](resources/RecycleItemsView_concept.png)

## How to use
### C#
```c#
var recycleView = new RecycleItemsView()
{
    ContentMargin = 60,
    ItemHeight = 350,
    ItemWidth = 300,
    Spacing = 20,
    ItemsSource = item,
    ItemTemplate = new DataTemplate(() =>
    {
        Label label;
        var view = new StackLayout {
            Children =
            {
                (label = new Label { })
            }
        };
        view.SetBinding(StackLayout.BackgroundColorProperty, new Binding("Color"));
        label.SetBinding(Label.TextProperty, new Binding("Label"));
        return view;
    })
}),
```
### XAML
``` xaml
<tvcontrols:RecycleItemsView ContentMargin="60" ItemWidth="300" ItemHeight="350" Spacing="20" ItemsSource="{Binding Items}">
    <tvcontrols:RecycleItemsView.ItemTemplate>
        <DataTemplate>
            <StackLayout BackgroundColor="{Binding Color}">
                <Label Text="{Binding Text}"/>
            </StackLayout>
        </DataTemplate>
    </tvcontrols:RecycleItemsView.ItemTemplate>
</tvcontrols:RecycleItemsView>
```
## Properties related layouting
![layouting](resources/RecycleItemsView_layouting.png)

### Item width and height
All items in RecycleItemsView have the same width and height because it should be reused.

## Multiple columns
You can display items on multiple lines. Use `ColumnCount` property to apply multiple columns
![colums](resources/RecycleItemsView_colums.gif)

## Header and Footer
 RecycleItemsView display items with same `DataTemplate`. if you want to use a special looks for first or last, you can use Header, Footer property

![footer](resources/RecycleItemsView_footer.png)
![footer2](resources/RecycleItemsView_footer2.png)

 The Header and Footer can be a data object that contain view model or a view instance.
 If Header or Footer is a View, it directly used for displaying header/footer. 
 If not, HeaderTemplate/FooterTemplate is used to make a view that used for displaying header/footer

## Related Links
 * [DataTemplate](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/templates/data-templates)
 * [Sample](https://github.com/Samsung/Tizen.TV.UIControls/tree/master/sample/Sample/RecycleItemsView)