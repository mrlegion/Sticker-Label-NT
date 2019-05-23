# Behaviors directory
Use this directory for create you behaviors classes using into UI elements
## Example behaviors class

``` csharp
// --------------------
// SomeoneBehavior.cs
// --------------------

public class SomeoneBehavior: IBehavior<T> where T : UIElement
{
    protected override void OnAttached()
    {
        base.OnAttached();
        this.AssociatedObject.SelectEvent += SomeoneEventHandler;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        this.AssociatedObject.SelectEvent -= SomeoneEventHandler;
    }

    private void SomeoneEventHandler(object sender, EventArgs e)
    {
        // someone code
    }
}    
```

Use in XAML:
``` xml
<!-- Link to Window or Page namespace -->
<Window xmlns:behaviors="clr-namespace:YouApp.UI.Common.Behaviors"
        xmlns:i="http://schemas.microsoft.com/expression/2010/interactivity">
    <!-- Someone elemet -->
    <TextBlock Text="Someone text">
        <i:Interaction.Behavior>
            <behaviors:SomeoneBehavior/>
        </i:Interaction.Behavior>
    </TextBlock>
<Window/>
```