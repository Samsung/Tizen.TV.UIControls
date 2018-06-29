RecycleItemsView is provide default focusing effect that is scaling to 1.2
If want to change this behavior, you need to inherit RecycleItemsView and override `OnItemFocused` method

``` C#
   protected virtual void OnItemFocused(object data, View targetView, bool isFocused);
```
 When need to apply focusing effect, We call this method with focused view

 * `data` is a FocusedItem in ItemsSource
 * `targetView` is a realized view that created by `ItemTemplate`
 * `isFocused` whether focus or not.

You need to implement `Focused` and `Unfocused` cases
If not, all views will be shown as focused

## Focus with header footer
Header/Footer also can get a focus and triggered OnItemFocused when focused. So, if you want know that focused view is header or not, compare data parameter with Header property

## Custom Fouse Effect example
``` xaml
<local:MyRecycleItemsView ...>
  <tvcontrols:RecycleItemsView.ItemTemplate>
    <DataTemplate>
      <AbsoluteLayout>
        <Image Source="{Binding Source}" Aspect="Fill" 
               AbsoluteLayout.LayoutBounds="0, 0, 1, 1"
               AbsoluteLayout.LayoutFlags="All"/>
        <StackLayout Padding="20" BackgroundColor="#aa000000"
               AbsoluteLayout.LayoutBounds="0, 1, 480, 100"
               AbsoluteLayout.LayoutFlags="PositionProportional">
          <Label Text="{Binding Text}" TextColor="AntiqueWhite" FontSize="70" FontAttributes="Bold" />
          <Label Text="{Binding DetailText}" FontSize="40"/>
        </StackLayout>
      </AbsoluteLayout>
    </DataTemplate>
  </tvcontrols:RecycleItemsView.ItemTemplate>
</local:MyRecycleItemsView>
```
``` C#
        protected override void OnItemFocused(object data, View targetView, bool isFocused)
        {
            AbsoluteLayout layout = (AbsoluteLayout)targetView;
            View textarea = layout.Children[1];
            if (isFocused)
            {
                targetView.ScaleTo(1.2);
                var animation = new Animation((rate) =>
                {
                    AbsoluteLayout.SetLayoutBounds(textarea, new Rectangle(0, 1, 480, 100 + rate * 100));
                });
                animation.Commit(this, $"Focused - {data.GetHashCode()}");
            }
            else
            {
                targetView.ScaleTo(1.0);
                var animation = new Animation((rate) =>
                {
                    AbsoluteLayout.SetLayoutBounds(textarea, new Rectangle(0, 1, 480, 200 - rate * 100));
                });
                animation.Commit(this, $"Focused - {data.GetHashCode()}");
            }
        }
```
When Item was focused, text area is grow up to 200px

We can know a View type because it was created by ItemTemplate
So, we can get a `StackLayout` object and make larger

![img](resources/RecycleItemsView_img.gif)