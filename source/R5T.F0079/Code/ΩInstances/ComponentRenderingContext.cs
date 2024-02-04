using System;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Mvc.Rendering;


namespace R5T.F0079
{
    public class ComponentRenderingContext
    {
        /// <summary>
        /// Services used to create the HtmlHelper.
        /// This set of services can be reused when creating multiple component renderers.
        /// </summary>
        public IServiceCollection Services { get; set; }
        /// <summary>
        /// A reference is kept so that the HtmlHelper is not disposed.
        /// </summary>
        public IServiceProvider HtmlHelperServiceProvider { get; set; }
        public IHtmlHelper HtmlHelper { get; set; }
    }
}
