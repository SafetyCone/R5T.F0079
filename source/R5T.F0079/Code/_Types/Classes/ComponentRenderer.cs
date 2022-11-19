using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Components;


namespace R5T.F0079
{
    public class ComponentRenderer<TComponent> : ComponentRenderer
        where TComponent : IComponent
    {
    }


    public class ComponentRenderer
    {
        public IServiceCollection Services { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}
