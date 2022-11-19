using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using R5T.D0118;
using R5T.D0118.I000;
using R5T.F0033;
using R5T.R0003;
using R5T.T0132;
using R5T.Z0015;


namespace R5T.F0079.Construction
{
	[FunctionalityMarker]
	public partial interface ITry : IFunctionalityMarker
	{
        public async Task ComponentWithServiceDependency()
        {
            var text = await ComponentOperator.Instance.NewRenderer<Component03>()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IServiceC, ServiceC01>();
                })
                .Render();

            NotepadPlusPlusOperator.Instance.WriteTextAndOpen(
                FilePaths.Instance.OutputTextFilePath,
                text);
        }

        public async Task ComponentWithParameters()
        {
            var text = await ComponentOperator.Instance.NewRenderer<Component05>()
                .SetParameter(c => c.Title, "The Title")
                .SetParameter(c => c.Person, new Component05.Name
                {
                    FirstName = "David",
                    LastName = "Coats",
                })
                .Render();

            NotepadPlusPlusOperator.Instance.WriteTextAndOpen(
                FilePaths.Instance.OutputTextFilePath,
                text);
        }

		public async Task SimpleComponent()
		{
            var text = await ComponentOperator.Instance.NewRenderer<Component09>()
                .Render();

            NotepadPlusPlusOperator.Instance.WriteTextAndOpen(
                FilePaths.Instance.OutputTextFilePath,
                text);
        }
	}
}