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
	public sealed class SaveDetailPresetListScreen : ListScreenBase<SaveDetailPresetDataModel>
	{
		protected override string GetScreenName() => "User Detail";
		protected override DataListViewModel<SaveDetailPresetDataModel>? GetListData() => Model;
		protected override IEnumerable<ListViewMapping> GetViewMappings()
		{
			RowViewMapping rowViewMapping = new RowViewMapping();
			rowViewMapping.Columns = (IEnumerable<ModelMapping>)new ModelMapping[3]
			{
				CreateMapping<string>((Expression<Func<SaveDetailPresetDataModel, string>>) (p => p.FirstName)),
				CreateMapping<string>((Expression<Func<SaveDetailPresetDataModel, string>>) (p => p.LastName)),
				CreateMapping<int>((Expression<Func<SaveDetailPresetDataModel, int>>) (p => p.Age))

			};
			return (IEnumerable<ListViewMapping>)new ListViewMapping[1]
			{
				(ListViewMapping) rowViewMapping
			};
		}
		protected override ActionBase GetListItemPrimaryAction(SaveDetailPresetDataModel model)
		{
			return NavigateScreenAction.To<SaveDetailPresetEditScreen>().With(new SaveDetailByIdQuery()
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
				NodeAction = NavigateScreenAction.To<SaveDetailPresetEditScreen>()
			};
		}
		protected override IEnumerable<ActionGroup>? GetListItemContextActions(SaveDetailPresetDataModel model)
		{
			return (IEnumerable<ActionGroup>)new ActionGroup[1]
			{
				new ActionGroup()
				{
					Nodes = new List<ActionNode>()
					{
						ActionBuilder.Edit<SaveDetailPresetEditScreen>((DataQueryBase) new SaveDetailByIdQuery()
						{
							Id = model.Id
						}),
						ActionBuilder.Delete((CommandBase) new DeleteSaveDetail()
						{
							Id = model.Id
						}, "Delete User?", "Do you want to delete User '" + model.FirstName + "'?")
					}
				}
			};
		}
	}
}
