using CustomAppModules.Api;
using CustomAppModules.Models;
using Dynamicweb.CoreUI.Data;
using Dynamicweb.Extensibility.Mapping;

namespace CustomAppModules.Queries
{
	public sealed class MapCategoryByIdQuery : DataQueryIdentifiableModelBase<MapCategoryPresetDataModel, int>
	{
		public int Id { get; set; }
		public override MapCategoryPresetDataModel GetModel()
		{
			var MapCategoryInfo = MapCategoryPresetService.GetMapCategoryById(Id);
			return MappingService.Map<MapCategoryPresetDataModel>(MapCategoryInfo);
		}

		protected override void SetKey(int key) => Id = key;
	}
}
