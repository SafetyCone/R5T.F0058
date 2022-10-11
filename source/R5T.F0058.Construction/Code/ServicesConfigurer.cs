using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using R5T.F0036.F001;


namespace R5T.F0058.Construction
{
    internal class ServicesConfigurer : IServicesConfigurer
    {
        public Task ConfigureServices(IServiceCollection services)
        {
            F0035.ServicesOperator.Instance.AddLogging(
                services,
                loggingBuilder =>
                {
                    // Do nothing else.
                });

            //services
            //    ;

            return Task.CompletedTask;
        }
    }
}
