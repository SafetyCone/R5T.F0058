using System;

using R5T.F0046;
using R5T.F0060;
using R5T.F0063;
using R5T.T0146;


namespace R5T.F0058
{
    public static class Instances
    {
        public static IReasonOperator ReasonOperator { get; } = T0146.ReasonOperator.Instance;
        public static IRepositoryNameOperator RepositoryNameOperator { get; } = F0046.RepositoryNameOperator.Instance;
        public static F0060.IRepositoryOperator RepositoryOperator { get; } = F0060.RepositoryOperator.Instance;
        public static IResultOperator ResultOperator { get; } = T0146.ResultOperator.Instance;
        public static ISolutionOperations SolutionOperations { get; } = F0063.SolutionOperations.Instance;
    }
}