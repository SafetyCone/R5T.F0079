using System;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Builder;

using R5T.T0132;


namespace R5T.F0079
{
	[FunctionalityMarker]
	public partial interface IServicesOperator : IFunctionalityMarker
	{
        public IServiceCollection GetWebApplicationServices()
        {
            var builder = WebApplication.CreateBuilder();

            var services = builder.Services;
			return services;
        }

        public IServiceCollection GetRazorComponentServices()
		{
			var services = this.GetWebApplicationServices();

			services.AddRazorPages();

			return services;
		}
	}
}