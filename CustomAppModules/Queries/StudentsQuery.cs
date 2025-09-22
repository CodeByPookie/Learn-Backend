using CustomAppModules.Api;
using CustomAppModules.Models;
using Dynamicweb.CoreUI.Data;

namespace CustomAppModules.Queries
{
	public sealed class StudentsQuery : DataQueryListBase<StudentsPresetDataModel, StudentsPreset>
	{
		protected override IEnumerable<Api.StudentsPreset> GetListItems()
		{
			IEnumerable<StudentsPreset> all = StudentsPresetService.GetStudentsPresets();
			return all;
		}
	}
}
