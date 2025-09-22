using CustomAppModules.Api;
using CustomAppModules.Models;
using Dynamicweb.CoreUI.Data;
using System.Data;

namespace CustomAppModules.Commands
{
	public sealed class AddSaveDetail : CommandBase<SaveDetailPresetDataModel>
	{
		public override CommandResult Handle()
		{
			if (Model is null) return new()
			{
				Status = CommandResult.ResultType.Invalid,
				Message = "Model data must be given"
			};
			var isNew = Model.Id == 0;
			var preset = isNew ? new() : SaveDetailPresetService.GetSaveDetailById(Model!.Id) ?? new();

			preset.FirstName = Model.FirstName;
			preset.LastName = Model.LastName;
			preset.Age = Model.Age;

			if (!SaveDetailPresetService.Save(preset)) return new()
			{
				Status = CommandResult.ResultType.Error,
				Message = "An unknown error occurred while saving the preset"
			};

			return new()
			{
				Status = CommandResult.ResultType.Ok,
				Model = Model
			};
		}
	}
}
