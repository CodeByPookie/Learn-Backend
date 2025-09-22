using CustomAppModules.Commands;
using CustomAppModules.Models;
using CustomAppModules.Queries;
using Dynamicweb.Application.UI.Helpers;
using Dynamicweb.CoreUI.Actions.Implementations;
using Dynamicweb.CoreUI.Actions;
using Dynamicweb.CoreUI.Data;
using Dynamicweb.CoreUI.Lists.ViewMappings;
using Dynamicweb.CoreUI.Lists;
using Dynamicweb.CoreUI.Screens;
using System.Linq.Expressions;
using Dynamicweb.CoreUI.Icons;

namespace CustomAppModules.Screens
{
	public sealed class StoreLocationPresetListScreen : ListScreenBase<StoreLocationPresetDataModel>
	{
		protected override string GetScreenName() => "Store Location";

		protected override DataListViewModel<StoreLocationPresetDataModel>? GetListData() => Model;

		protected override IEnumerable<ListViewMapping> GetViewMappings()
		{
			RowViewMapping rowViewMapping = new RowViewMapping();
			rowViewMapping.Columns = (IEnumerable<ModelMapping>)new ModelMapping[5]
			{

				CreateMapping<string>((Expression<Func<StoreLocationPresetDataModel, string>>) (p => p.storeName)),
				CreateMapping<string>((Expression<Func<StoreLocationPresetDataModel, string>>) (p => p.City)),
				CreateMapping<string>((Expression<Func<StoreLocationPresetDataModel, string>>) (p => p.Zipcode)),
				CreateMapping<string>((Expression<Func<StoreLocationPresetDataModel, string>>) (p => p.PhoneNo)),
				CreateMapping<string>((Expression<Func<StoreLocationPresetDataModel, string>>) (p => p.StoreContactemail))
			};
			return (IEnumerable<ListViewMapping>)new ListViewMapping[1]
			{
				(ListViewMapping) rowViewMapping
			};
		}

		protected override ActionBase GetListItemPrimaryAction(StoreLocationPresetDataModel model)
		{
			return NavigateScreenAction.To<StoreLocationPresetEditScreen>().With(new StoreLocationByIdQuery()
			{
				Id = model.Id
			});
		}

		protected override ActionNode GetItemCreateAction()
		{
			return new ActionNode
			{
				Name = "Add New",
				Icon = Icon.PlusSquare,
				NodeAction = NavigateScreenAction.To<StoreLocationPresetEditScreen>()
			};
		}

		protected override IEnumerable<ActionGroup>? GetListItemContextActions(StoreLocationPresetDataModel model)
		{
			return (IEnumerable<ActionGroup>)new ActionGroup[1]
			{
				new ActionGroup()
				{
					Nodes = new List<ActionNode>()
					{
						ActionBuilder.Edit<StoreLocationPresetEditScreen>((DataQueryBase) new StoreLocationByIdQuery()
						{
							Id = model.Id
						}),
						ActionBuilder.Delete((CommandBase) new DeleteStoreLocationPresetCommand()
						{
							Id = model.Id
						}, "Delete store?", "Do you want to delete store '" + model.storeName + "'?")
					}
				}
			};
		}
	}
}
