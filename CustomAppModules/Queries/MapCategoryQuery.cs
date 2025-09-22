using CustomAppModules.Api;
using CustomAppModules.Models;
using Dynamicweb.CoreUI.Data;

namespace CustomAppModules.Queries
{
	public sealed class MapCategoryQuery : DataQueryListBase<MapCategoryPresetDataModel, MapCategoryPreset>
	{
		protected override IEnumerable<Api.MapCategoryPreset> GetListItems()
		{
			IEnumerable<MapCategoryPreset> all = MapCategoryPresetService.GetMapCategory();
			return all;
		}
	}
}
