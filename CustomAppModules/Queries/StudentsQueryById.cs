using CustomAppModules.Api;
using CustomAppModules.Models;
using Dynamicweb.CoreUI.Data;
using Dynamicweb.Extensibility.Mapping;

namespace CustomAppModules.Queries
{
	public sealed class StudentsQueryById : DataQueryIdentifiableModelBase<StudentsPresetDataModel, int>
	{
		public int Id { get; set; }
		public override StudentsPresetDataModel GetModel()
		{
			var saveDetail = StudentsPresetService.GetStudentById(Id);
			return MappingService.Map<StudentsPresetDataModel>(saveDetail);
		}
		protected override void SetKey(int key) => Id = key;
	}
}
