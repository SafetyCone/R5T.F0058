using System;
using System.Threading.Tasks;

using R5T.F0037;


namespace R5T.F0058.Construction
{
    partial class Program : IAsynchronousProgram
    {
        static async Task Main()
        {
            await F0037.Instances.Program
                .ConfigureServices(servicesBuilder =>
                {
                    servicesBuilder.UseServicesConfigurer<ServicesConfigurer>();
                })
                .Run<Program>();
        }
    }
}