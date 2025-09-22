using CustomAppModules.Api;
using CustomAppModules.Models;
using Dynamicweb.CoreUI.Data;
using Dynamicweb.Extensibility.Mapping;

namespace CustomAppModules.Queries
{
	public sealed class SaveDetailByIdQuery : DataQueryIdentifiableModelBase<SaveDetailPresetDataModel, int>
	{
		public int Id { get; set; }
		public override SaveDetailPresetDataModel GetModel()
		{
			var saveDetail = SaveDetailPresetService.GetSaveDetailById(Id);
			return MappingService.Map<SaveDetailPresetDataModel>(saveDetail);
		}
		protected override void SetKey(int key) => Id = key;
	}
}
