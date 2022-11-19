# R5T.F0079
Render Razor components to string functionality.

Inspiration is drawn from the BlazorTemplater NuGet package.

But the way components are rendered owes to this Stack Overflow answer: https://stackoverflow.com/a/66110767/10658484


## LIMITATIONS

Fantasically annoying is that the Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper.RenderComponentAsync() cannot handle layouts, as in the @layout directive is ignored.
However, the ChildContent RenderFragment convention *is* followed.
=> So as a workaround, just create components that use a "layout" component with their content being the component of interest. This means that for every component you want to have a layout, you need two components: 1) the component, 2) the "parent" component that includes the "layout" component wrapped around the component of interest.

Additionally, while the BlazorTemplater code does respect the @layout parameter for a component itself, it does *not* respect @layout directives in child components, lame. :(
This is why BlazorTemplater is not used.


## Prior Work

* R5T.Q0004 - Examinations of the BlazorTemplater NuGet package.
