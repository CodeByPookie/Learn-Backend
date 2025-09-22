using CustomAppModules.Api;
using CustomAppModules.Models;
using Dynamicweb.CoreUI.Data;
using Dynamicweb.Extensibility.Mapping;

namespace CustomAppModules.Queries
{
	public sealed class StoreLocationByIdQuery : DataQueryIdentifiableModelBase<StoreLocationPresetDataModel, int>
	{
		public int Id { get; set; }

		public override StoreLocationPresetDataModel GetModel()
		{
			var storeInfo = StoreLocationPresetService.GetStoreLocationById(Id);
			return MappingService.Map<StoreLocationPresetDataModel>(storeInfo);
		}

		protected override void SetKey(int key) => Id = key;
	}
}
