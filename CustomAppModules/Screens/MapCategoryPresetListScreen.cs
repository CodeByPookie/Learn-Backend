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
	public sealed class MapCategoryPresetListScreen : ListScreenBase<MapCategoryPresetDataModel>
	{
		protected override string GetScreenName() => "Map Category";
		protected override DataListViewModel<MapCategoryPresetDataModel>? GetListData() => Model;

		protected override IEnumerable<ListViewMapping> GetViewMappings()
		{
			RowViewMapping rowViewMapping = new RowViewMapping();
			rowViewMapping.Columns = (IEnumerable<ModelMapping>)new ModelMapping[3]
			{

				CreateMapping<string>((Expression<Func<MapCategoryPresetDataModel, string>>) (p => p.DWGroupName)),
				CreateMapping<string>((Expression<Func<MapCategoryPresetDataModel, string>>) (p => p.DWGroupId)),
				CreateMapping<string>((Expression<Func<MapCategoryPresetDataModel, string>>) (p => p.DemoGroupId))
			};
			return (IEnumerable<ListViewMapping>)new ListViewMapping[1]
			{
				(ListViewMapping) rowViewMapping
			};
			
		}
		protected override ActionBase GetListItemPrimaryAction(MapCategoryPresetDataModel model)
		{
			return NavigateScreenAction.To<MapCategoryPresetEditScreen>().With(new MapCategoryByIdQuery()
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
				NodeAction = NavigateScreenAction.To<MapCategoryPresetEditScreen>()
			};
		}
		protected override IEnumerable<ActionGroup>? GetListItemContextActions(MapCategoryPresetDataModel model)
		{
			return (IEnumerable<ActionGroup>)new ActionGroup[1]
			{
				new ActionGroup()
				{
					Nodes = new List<ActionNode>()
					{
						ActionBuilder.Edit<MapCategoryPresetEditScreen>((DataQueryBase) new MapCategoryByIdQuery()
						{
							Id = model.Id
						}),
						ActionBuilder.Delete((CommandBase) new DeleteMapCategory()
						{
							Id = model.Id
						}, "Delete Group?", "Do you want to delete group '" + model.DWGroupName + "'?")
					}
				}
			};
		}
	}
}
