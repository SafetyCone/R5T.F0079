# R5T.F0079 - OBSOLETE
Render Razor components to string functionality. (net6.0)

NO LONGER WORKS as of net8.0, you receive a nasty error:

System.InvalidOperationException: 'The current thread is not associated with the Dispatcher. Use InvokeAsync() to switch execution to the Dispatcher when triggering rendering or component state.'

See updated work for net8.0 in: R5T.F0144.

Inspiration is drawn from the BlazorTemplater NuGet package.

But the way components are rendered owes to this Stack Overflow answer: https://stackoverflow.com/a/66110767/10658484


## OBSOLETE

See R5T.F0144 (for net8.0).


## LIMITATIONS

No longer works as-of net8.0 (entry-point console application target framework of net8.0).

Fantasically annoying is that the Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper.RenderComponentAsync() cannot handle layouts, as in the @layout directive is ignored.
However, the ChildContent RenderFragment convention *is* followed.
=> So as a workaround, just create components that use a "layout" component with their content being the component of interest. This means that for every component you want to have a layout, you need two components: 1) the component, 2) the "parent" component that includes the "layout" component wrapped around the component of interest.

Additionally, while the BlazorTemplater code does respect the @layout parameter for a component itself, it does *not* respect @layout directives in child components, lame. :(
This is why BlazorTemplater is not used.


## Prior Work

* R5T.Q0004 - Examinations of the BlazorTemplater NuGet package.
