using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Components;


namespace R5T.F0079
{
    public static class ComponentRendererExtensions
    {
        public static TComponentRenderer ConfigureServices<TComponentRenderer>(this TComponentRenderer componentRenderer,
            Action<IServiceCollection> servicesAction)
            where TComponentRenderer : ComponentRenderer
        {
            Operations.Instance.ConfigureServices(componentRenderer, servicesAction);

            return componentRenderer;
        }

        public static async Task<TComponentRenderer> ModifyWith<TComponentRenderer>(this TComponentRenderer componentRenderer,
            Func<TComponentRenderer, Task> componentRendererAction)
            where TComponentRenderer : ComponentRenderer
        {
            await Operations.Instance.Modify(
                componentRenderer,
                componentRendererAction);

            return componentRenderer;
        }

        public static TComponentRenderer ModifyWith<TComponentRenderer>(this TComponentRenderer componentRenderer,
            Action<TComponentRenderer> componentRendererAction)
            where TComponentRenderer : ComponentRenderer
        {
            Operations.Instance.Modify(
                componentRenderer,
                componentRendererAction);

            return componentRenderer;
        }

        public static async Task<string> Render<TComponent>(this ComponentRenderer<TComponent> componentRenderer)
            where TComponent : IComponent
        {
            var output = await Operations.Instance.Render(componentRenderer);
            return output;
        }

        public static ComponentRenderer<TComponent> SetParameter<TComponent, TValue>(this ComponentRenderer<TComponent> componentRenderer,
            Expression<Func<TComponent, TValue>> parameterSelector,
            TValue value)
            where TComponent : IComponent
        {
            Operations.Instance.SetParameter(componentRenderer, parameterSelector, value);

            return componentRenderer;
        }
    }
}
