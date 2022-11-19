using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;

using R5T.T0132;


namespace R5T.F0079
{
	[FunctionalityMarker]
	public partial interface IComponentOperator : IFunctionalityMarker
	{
        public ComponentRenderingContext NewRenderingContext()
        {
            var htmlHelperServices = ServicesOperator.Instance.GetRazorComponentServices();

            var htmlHelperServiceProvider = htmlHelperServices.BuildServiceProvider();

            var htmlHelper = htmlHelperServiceProvider.GetService<IHtmlHelper>();

            var componentRenderingContext = new ComponentRenderingContext
            {
                HtmlHelper = htmlHelper,
                HtmlHelperServiceProvider = htmlHelperServiceProvider,
                Services = htmlHelperServices,
            };

            return componentRenderingContext;
        }

        public ComponentRenderer<TComponent> NewRenderer<TComponent>(IServiceCollection services)
           where TComponent : IComponent
        {
            var componentRenderer = new ComponentRenderer<TComponent>
            {
                Services = services,
                Parameters = new Dictionary<string, object>()
            };

            return componentRenderer;
        }

        public ComponentRenderer<TComponent> NewRenderer<TComponent>()
            where TComponent : IComponent
        {
            var razorComponentServices = ServicesOperator.Instance.GetRazorComponentServices();

            var componentRenderer = this.NewRenderer<TComponent>(razorComponentServices);
            return componentRenderer;
        }

        public ComponentRenderer<TComponent> NewRenderer<TComponent>(ComponentRenderingContext componentRenderingContext)
            where TComponent : IComponent
        {
            // Make a copy of the services set, so that when services are added for each component, they do not change the original services set in the rendering context.
            var servicesCopy = new ServiceCollection();
            
            servicesCopy.AddRange(componentRenderingContext.Services);

            var componentRenderer = this.NewRenderer<TComponent>(servicesCopy);
            return componentRenderer;
        }
    }
}