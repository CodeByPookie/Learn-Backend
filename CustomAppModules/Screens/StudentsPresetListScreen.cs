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
	public sealed class StudentsPresetListScreen : ListScreenBase<StudentsPresetDataModel> 
	{
		protected override string GetScreenName() => "Students Detail";
		protected override DataListViewModel<StudentsPresetDataModel>? GetListData() => Model;
		protected override IEnumerable<ListViewMapping> GetViewMappings()
		{
			RowViewMapping rowViewMapping = new RowViewMapping();
			rowViewMapping.Columns = (IEnumerable<ModelMapping>)new ModelMapping[3]
			{
				CreateMapping<string>((Expression<Func<StudentsPresetDataModel, string>>) (p => p.StudentName)),
				CreateMapping<string>((Expression<Func<StudentsPresetDataModel, string>>) (p => p.StudentClass)),
				CreateMapping<int>((Expression<Func<StudentsPresetDataModel, int>>) (p => p.StudentAge))
			};
			return (IEnumerable<ListViewMapping>)new ListViewMapping[1]
			{
				(ListViewMapping) rowViewMapping
			};
		}
		protected override ActionBase GetListItemPrimaryAction(StudentsPresetDataModel model)
		{
			return NavigateScreenAction.To<StudentsPresetEditScreen>().With(new StudentsQueryById()
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
				NodeAction = NavigateScreenAction.To<StudentsPresetEditScreen>()
			};
		}
		protected override IEnumerable<ActionGroup>? GetListItemContextActions(StudentsPresetDataModel model)
		{
			return (IEnumerable<ActionGroup>)new ActionGroup[1]
			{
				new ActionGroup()
				{
					Nodes = new List<ActionNode>()
					{
						ActionBuilder.Edit<StudentsPresetEditScreen>((DataQueryBase) new StudentsQueryById()
						{
							Id = model.Id
						}),
						ActionBuilder.Delete((CommandBase) new DeleteSaveDetail()
						{
							Id = model.Id
						}, "Delete Student?", "Do you want to delete Student '" + model.StudentName + "'?")
					}
				}
			};
		}
	}
}
