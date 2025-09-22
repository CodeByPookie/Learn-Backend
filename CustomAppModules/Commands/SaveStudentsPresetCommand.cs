using CustomAppModules.Api;
using CustomAppModules.Models;
using Dynamicweb.CoreUI.Data;

namespace CustomAppModules.Commands
{
	public sealed class SaveStudentsPresetCommand : CommandBase<StudentsPresetDataModel>
	{
		public override CommandResult Handle()
		{
			if (Model is null) return new()
			{
				Status = CommandResult.ResultType.Invalid,
				Message = "Model data must be given"
			};
			var isNew = Model.Id == 0;
			var preset = isNew ? new() : StudentsPresetService.GetStudentById(Model!.Id) ?? new();

			preset.StudentName = Model.StudentName;
			preset.StudentClass = Model.StudentClass;
			preset.StudentAge = Model.StudentAge;
			preset.StudentEmail = Model.StudentEmail;
			preset.StudentPhone = Model.StudentPhone;
			preset.StudentAddress = Model.StudentAddress;

			if (!StudentsPresetService.Save(preset)) return new()
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
