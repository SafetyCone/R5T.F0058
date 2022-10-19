using System;
using System.Threading.Tasks;


namespace R5T.F0058.Construction
{
    partial class Program
    {
        public async Task Run()
        {
            await Instances.RepositoryOperations.Delete_Idempotent();
            //await Instances.RepositoryOperations.CreateNew_MinimalRepository_NonIdempotent();
            //await Instances.RepositoryOperations.CreateNew_ProgramAsService_ConsoleRepository();
        }
    }
}