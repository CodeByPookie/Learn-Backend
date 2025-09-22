using CustomAppModules.Api;
using Dynamicweb.CoreUI.Data;


namespace CustomAppModules.Commands
{
	public sealed class DeleteStudentsPresetCommand : CommandBase
	{
		public int Id { get; set; }

		public override CommandResult Handle() => StudentsPresetService.Delete(Id)
			? new() { Status = CommandResult.ResultType.Ok }
			: new() { Status = CommandResult.ResultType.Error, Message = "An unknown error occurred while trying to delete the data" };
	}
}
