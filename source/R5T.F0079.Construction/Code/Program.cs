using System;
using System.Threading.Tasks;


namespace R5T.F0079.Construction
{
    class Program
    {
        static async Task Main()
        {
            //await Try.Instance.SimpleComponent();
            //await Try.Instance.ComponentWithParameters();
            await Try.Instance.ComponentWithServiceDependency();
        }
    }
}