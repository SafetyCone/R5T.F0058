using System;

using R5T.F0046;
using R5T.F0061;
using R5T.Z0014;
using R5T.Z0015;


namespace R5T.F0058.Construction
{
    public static class Instances
    {
        public static IFilePaths FilePaths { get; } = Z0015.FilePaths.Instance;
        public static IOperations Operations { get; } = F0061.Operations.Instance;
        public static IRepositoryDescriptions RepositoryDescriptions { get; } = Z0014.RepositoryDescriptions.Instance;
        public static IRepositoryNames RepositoryNames { get; } = Z0014.RepositoryNames.Instance;
        public static IRepositoryOperations RepositoryOperations { get; } = Construction.RepositoryOperations.Instance;
        public static F0060.IRepositoryOperator RepositoryOperator { get; } = F0060.RepositoryOperator.Instance;
    }
}