using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.T0132;


namespace R5T.F0058.Construction
{
	[FunctionalityMarker]
	public partial interface IRepositoryOperations : IFunctionalityMarker,
		F0058.IRepositoryOperations
	{
		public async Task CreateNew_ProgramAsService_ConsoleRepository()
        {
			/// Inputs.
			var name =
				//"R5T.W1001"
				Instances.RepositoryNames.TestRepository
				;
			var description =
				//"A private first repo for creating a web application."
				Instances.RepositoryDescriptions.ForTestRepository
				;
			var isPrivate = true;

			var owner =
				F0047.Instances.GitHubOwners.SafetyCone
				;

			/// Run.
			await using var services = F0036.Instances.ServiceProvider
				.ConfigureServices(servicesBuilder =>
				{
					servicesBuilder.UseServicesConfigurer<ServicesConfigurer>();
				})
				.Build();

			var logger = services.GetLogger(nameof(CreateNew_ProgramAsService_ConsoleRepository));

			await this.CreateNew_ProgramAsService_ConsoleRepository(
				owner,
				name,
				description,
				isPrivate,
				logger);
		}

		/// <summary>
		/// Create a new remote repository, clones it locally, adds the minimal files and folders for a repository, then commits the changes.
		/// </summary>
		public async Task CreateNew_MinimalRepository_NonIdempotent()
		{
			/// Inputs.
			var name =
				//"R5T.W1001"
				Instances.RepositoryNames.TestRepository
				;
			var description =
				//"A private first repo for creating a web application."
				Instances.RepositoryDescriptions.ForTestRepository
				;
			var isPrivate = true;

			var owner =
				F0047.Instances.GitHubOwners.SafetyCone
				;


			/// Run.
			await using var services = F0036.Instances.ServiceProvider
				.ConfigureServices(servicesBuilder =>
				{
					servicesBuilder.UseServicesConfigurer<ServicesConfigurer>();
				})
				.Build();

			var logger = services.GetLogger(nameof(CreateNew_MinimalRepository_NonIdempotent));

			/// Library.
			var libraryDescriptors = F0043.LibraryOperator.Instance.GetDescriptors(
				name,
				description,
				isPrivate,
				logger);

			/// Repository.
			var repositoryDescriptors = Instances.RepositoryOperator.GetDescriptors(
				libraryDescriptors.Name,
				libraryDescriptors.Description,
				owner);

			var result = await this.CreateNew_MinimalRepository_NonIdempotent(
				owner,
				repositoryDescriptors.Name,
				repositoryDescriptors.Description,
				isPrivate,
				logger);

			// Output result to file, then open in Notepad++.
			Instances.Operations.WriteResultAndOpenInNotepadPlusPlus(
				result,
				Instances.FilePaths.ResultOutputJsonFilePath,
				logger);
		}

		public async Task Delete_Idempotent()
		{
			/// Inputs.
			var name =
                //"R5T.F0051"
                Instances.RepositoryNames.TestRepository + ".Private"
                ;
			var owner =
				F0047.Instances.GitHubOwners.SafetyCone
				;
			var isPrivate = true;

			/// Run.
			await using var services = F0036.Instances.ServiceProvider
				.ConfigureServices(servicesBuilder =>
				{
					servicesBuilder.UseServicesConfigurer<ServicesConfigurer>();
				})
				.Build();

			var logger = services.GetLogger(nameof(CreateNew_MinimalRepository_NonIdempotent));

			var deleteRepositoryResult = await this.Delete_Idempotent(
				name,
				owner,
				isPrivate,
				logger);

			// Output result to file, then open in Notepad++.
			Instances.Operations.WriteResultAndOpenInNotepadPlusPlus(
				deleteRepositoryResult,
				Instances.FilePaths.ResultOutputJsonFilePath,
				logger);
		}
	}
}