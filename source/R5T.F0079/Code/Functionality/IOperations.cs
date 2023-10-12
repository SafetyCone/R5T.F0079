using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using R5T.T0132;
using Microsoft.VisualBasic;

namespace R5T.F0079
{
    [FunctionalityMarker]
    public partial interface IOperations : IFunctionalityMarker
    {
        public void ConfigureServices(
            ComponentRenderer componentRenderer,
            Action<IServiceCollection> servicesAction)
        {
            servicesAction(componentRenderer.Services);
        }

        public HttpContext CreateDefaultHttpContext(IServiceProvider serviceProvider)
        {
            return new DefaultHttpContext
            {
                RequestServices = serviceProvider,
                Request =
                {
                  Scheme = "http",
                  Host = new HostString("localhost"),
                  PathBase = "/base",
                  Path = "/path",
                  QueryString = QueryString.FromUriComponent("?query=value")
                }
            };
        }

        // Adapted from: https://github.com/conficient/BlazorTemplater/blob/91fa80058dde63e8866642460a587d0520377976/BlazorTemplater/ComponentRenderer.cs#L99
        public string GetParameterName<TComponent, TValue>(Expression<Func<TComponent, TValue>> parameterSelector)
            where TComponent : IComponent
        {
            if (false
                || parameterSelector.Body is not MemberExpression memberExpression
                || memberExpression.Member is not PropertyInfo propInfoCandidate)
            {
                throw new ArgumentException($"The parameter selector '{parameterSelector}' does not resolve to a public property on the component '{typeof(TComponent)}'.", nameof(parameterSelector));
            }

            var componentType = typeof(TComponent);

            var declaringTypeIsComponent = propInfoCandidate.DeclaringType == componentType;

            var propertyInfo = declaringTypeIsComponent
                ? componentType.GetProperty(propInfoCandidate.Name, propInfoCandidate.PropertyType)
                : propInfoCandidate
                ;

            var attribute = propertyInfo?.GetCustomAttribute<ParameterAttribute>(inherit: true);

            var attributeSelectFailed = false
                || propertyInfo is null
                || attribute is null
                ;

            if (attributeSelectFailed)
            {
                throw new ArgumentException($"The parameter selector '{parameterSelector}' does not resolve to a public property on the component '{typeof(TComponent)}' with a [Parameter] or [CascadingParameter] attribute.", nameof(parameterSelector));
            }

            var output = propertyInfo.Name;
            return output;
        }

        public async Task<TComponentRenderer> Modify<TComponentRenderer>(
            TComponentRenderer componentRenderer,
            Func<TComponentRenderer, Task> componentRendererAction)
            where TComponentRenderer : ComponentRenderer
        {
            await F0000.ActionOperator.Instance.Run_OkIfDefault(
                componentRenderer,
                componentRendererAction);

            return componentRenderer;
        }

        public TComponentRenderer Modify<TComponentRenderer>(
            TComponentRenderer componentRenderer,
            Action<TComponentRenderer> componentRendererAction)
            where TComponentRenderer : ComponentRenderer
        {
            Instances.ActionOperator.Run_OkIfDefault(
                componentRenderer,
                componentRendererAction);

            return componentRenderer;
        }

        public async Task<string> Render<TComponent>(ComponentRenderer<TComponent> componentRenderer)
            where TComponent : IComponent
        {
            var serviceProvider = componentRenderer.Services.BuildServiceProvider();

            var helper = serviceProvider.GetService<IHtmlHelper>();

            var context = this.CreateDefaultHttpContext(serviceProvider);

            var output = await this.Render<TComponent>(
                helper,
                context,
                componentRenderer.Parameters);

            return output;
        }

        public async Task<string> Render<TComponent>(IHtmlHelper helper, HttpContext httpContext, object parameters)
            where TComponent : IComponent
        {
            if (helper is IViewContextAware viewContextAware)
            {
                viewContextAware.Contextualize(new ViewContext()
                {
                    HttpContext = httpContext
                });
            }

            var content = await helper.RenderComponentAsync<TComponent>(RenderMode.Static, parameters);

            var writer = new StringWriter();

            content.WriteTo(writer, HtmlEncoder.Default);

            var output = writer.ToString();
            return output;
        }

        public void SetParameter<TComponent, TValue>(
            ComponentRenderer<TComponent> componentRenderer,
            Expression<Func<TComponent, TValue>> parameterSelector,
            TValue value)
            where TComponent : IComponent
        {
            var parameterName = this.GetParameterName(parameterSelector);

            componentRenderer.Parameters.Add(parameterName, value);
        }
    }
}