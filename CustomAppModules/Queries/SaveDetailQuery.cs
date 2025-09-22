using CustomAppModules.Api;
using CustomAppModules.Models;
using Dynamicweb.CoreUI.Data;
using Dynamicweb.Extensibility.Mapping;

namespace CustomAppModules.Queries
{
	public sealed class SaveDetailQuery : DataQueryListBase<SaveDetailPresetDataModel, SaveDetailPreset>
	{
		protected override IEnumerable<Api.SaveDetailPreset> GetListItems()
		{
			IEnumerable<SaveDetailPreset> all = SaveDetailPresetService.GetSaveDataPresets();
			return all;
		}
	}
}