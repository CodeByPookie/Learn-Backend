using CustomAppModules.Api;
using CustomAppModules.Models;
using Dynamicweb.CoreUI.Data;

namespace CustomAppModules.Queries
{
	public sealed class StoreLocationQuery : DataQueryListBase<StoreLocationPresetDataModel, StoreLocationPreset>
	{
		protected override IEnumerable<Api.StoreLocationPreset> GetListItems()
		{
			IEnumerable<StoreLocationPreset> all = StoreLocationPresetService.GetStoreLocations();
			return all;
		}		
	}

	//public sealed class StoreLocationQuery : DataQueryModelBase<DataListViewModel<StoreLocationPresetDataModel>>
	//{
	//    public override DataListViewModel<StoreLocationPresetDataModel> GetModel()
	//    {
	//        //var all = StoreLocationPresetService.GetStoreLocations();
	//        //return (DataListViewModel<StoreLocationPresetDataModel>)MappingService.Map<IEnumerable<StoreLocationPresetDataModel>>(all);

	//        IEnumerable<StoreLocationPreset> all = StoreLocationPresetService.GetStoreLocations();
	//        int count = 0;
	//        if (all != null)
	//        {
	//            count = all.Count();
	//        }
	//        return new DataListViewModel<StoreLocationPresetDataModel>()
	//        {
	//            Data = MappingService.Map<IEnumerable<StoreLocationPresetDataModel>>(all),
	//            TotalCount = count
	//        };
	//    }
	//}
}
