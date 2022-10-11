using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.F0042;
using R5T.T0132;
using R5T.T0146;


namespace R5T.F0058
{
	[FunctionalityMarker]
	public partial interface IRepositoryOperations : IFunctionalityMarker,
		F0045.IRepositoryOperations
	{
		/// <summary>
		/// Create a new remote repository, clones it locally, adds the minimal files and folders for a repository, then commits the changes.
		/// </summary>
		public async Task<Result<RepositoryLocationsPair>> CreateNew_MinimalRepository_NonIdempotent(
			string owner,
			string repositoryName,
			string description,
			bool isPrivate,
			ILogger logger)
		{
			/// Run.
			var ownedRepositoryName = Instances.RepositoryNameOperator.GetOwnedRepositoryName(owner, repositoryName); 

			var result = T0146.Instances.ResultOperator.Result<RepositoryLocationsPair>()
				.WithTitle("Create New Minimal Repository");

			async Task<Result> PerformSafetyChecks()
            {
				var safetyChecksResult = Instances.ResultOperator.Result()
					.WithTitle("Perform safety checks.");

				async Task<bool> Internal()
				{
					var verifyRepositoryAvailabilityResult = await Instances.RepositoryOperator.Verify_RepositoryDoesNotExist(
						owner,
						repositoryName,
						logger);

					var verifyRepositoryAvailabilityReason = verifyRepositoryAvailabilityResult.ToReason(
						$"Repository does not exist: {ownedRepositoryName}.",
						$"Repository already exists: {ownedRepositoryName}.");

					safetyChecksResult
						.WithReasons(verifyRepositoryAvailabilityReason)
						.WithChildren(verifyRepositoryAvailabilityResult);

					var success = verifyRepositoryAvailabilityResult.IsSuccess();
					return success;
				}

				await logger.InSuccessFailureLogContext_Error(
					"Performing safety checks...",
					"Safety checks passed.",
					"Failed safety checks.",
					Internal);

				return safetyChecksResult;
            }

			var safetyChecksResult = await PerformSafetyChecks();

			IReason safetyChecksReason = safetyChecksResult.ToReason(
				"Passed safety checks.",
				"Failed safety checks.");

			result
				.WithReasons(safetyChecksReason)
				.WithChildren(safetyChecksResult);

			// If safety checks failed, return and do not create a repository.
			if(safetyChecksResult.IsFailure())
            {
				return result;
            }

			logger.LogInformation($"Remote GitHub repository '{repositoryName}' does not already exist.");

			// Create new.
			var repositorySpecification = Instances.RepositoryOperator.Get_RepositorySpecification(
				owner,
				repositoryName,
				description,
				isPrivate);

			var createNewResult = await Instances.RepositoryOperator.CreateNew_NonIdempotent(
				repositorySpecification,
				logger);

			IReason createNewReason = createNewResult.ToReason(
				"Created new repository.",
				"Failed to create new repository.");

			result
				.WithReason(createNewReason)
				.WithChild(createNewResult)
				;

			var localRepositoryDirectoryPath = createNewResult.Value;

			// Setup repository (gitignore file, source directory).
			var setupRepositoryResult = Instances.RepositoryOperator.SetupRepository(
				localRepositoryDirectoryPath,
				logger);

			var setupRepositoryReason = setupRepositoryResult.ToReason(
				"Successfully setup repository.",
				"Failed to setup repository.");

			result.WithReason(setupRepositoryReason).WithChild(setupRepositoryResult);

			// Perform initial commit.
			var performInitialCommitResult = Instances.RepositoryOperator.PerformInitialCommit(
				localRepositoryDirectoryPath,
				logger);

			IReason performInitialCommitReason = performInitialCommitResult.ToReason(
				"Initial commit succeeded.",
				"Initial commit failed.");

			result.WithReason(performInitialCommitReason).WithChild(performInitialCommitResult);

			// Return the result.
			return result;
		}

		public async Task<Result> Delete_Idempotent(
			string name,
			string owner,
			bool isPrivate,
			ILogger logger)
		{
			/// Library.
			var libraryName = F0043.LibraryOperator.Instance.GetLibraryName(
				name,
				isPrivate,
				logger);

			/// Repository.
			var repositoryName = Instances.RepositoryOperator.GetRepositoryName(libraryName);

			var ownedRepositoryName = Instances.RepositoryOperator.GetOwnedRepositoryName(
				repositoryName,
				owner);

			var deleteRepositoryResult = await logger.InLogContext(
				$"Deleting repository {ownedRepositoryName}...",
				$"Deleted repository {ownedRepositoryName}.",
				() => Instances.RepositoryOperator.Delete_Idempotent(
					repositoryName,
					owner,
					logger));

			return deleteRepositoryResult;
		}
	}
}