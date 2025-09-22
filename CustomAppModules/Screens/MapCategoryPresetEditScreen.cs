using CustomAppModules.Commands;
using CustomAppModules.Models;
using Dynamicweb.CoreUI.Data;
using Dynamicweb.CoreUI.Editors.Inputs;
using Dynamicweb.CoreUI.Editors.Lists;
using Dynamicweb.CoreUI.Editors;
using Dynamicweb.CoreUI.Screens;
using Dynamicweb.Data;
using System.Data;
using System.Linq.Expressions;
using Dynamicweb.Core;

namespace CustomAppModules.Screens
{
	public sealed class MapCategoryPresetEditScreen : EditScreenBase<MapCategoryPresetDataModel>
	{
		protected override void BuildEditScreen()
		{
			if (!string.IsNullOrEmpty(Model.DWGroupName))
			{
				// lookup GroupId from DB
				var sql = CommandBuilder.Create(@"
					SELECT TOP 1 g.GroupId
					FROM EcomGroups g
					INNER JOIN EcomShopGroupRelation sgr ON g.GroupId = sgr.ShopGroupGroupId
					WHERE g.GroupName = {0} AND sgr.ShopGroupShopId = {1}",
					Model.DWGroupName, "SHOP1");

				using var reader = Database.CreateDataReader(sql);
				if (reader.Read())
				{
					Model.DWGroupId = reader["GroupId"]?.ToString();
				}
			}
			// Add components to the screen
			AddComponents("Map Category", new LayoutWrapper[]
			{
				new("Map Category", new[]
				{
					EditorFor(m => m.DWGroupName),
					EditorFor(m => m.DWGroupId),
					EditorFor(m => m.DemoGroupId)
				})
			});
		}
		protected override EditorBase? GetEditor(string property)
		{
			EditorBase editor;

			switch (property)
			{
				case "DWGroupName":
					editor = (EditorBase)CreateGroupNameSelect();
					break;

				case "DWGroupId":
					editor = (EditorBase)CreateGroupIdSelect();
					break;

				case "DemoGroupId":
					editor = new Textarea();
					break;

				default:
					editor = null;
					break;
			}

			return editor;
		}
				
		private static Select CreateGroupNameSelect()
		{
			var editor = new Select
			{
				ShowNothingSelectedOption = true,
				ReloadOnChange = true
			};
			
			using (IDataReader dataReader = Database.CreateDataReader(@"
					SELECT DISTINCT g.GroupId, g.GroupName
					FROM EcomGroups g
					INNER JOIN EcomShopGroupRelation sgr ON g.GroupId = sgr.ShopGroupGroupId
					WHERE sgr.ShopGroupShopId = 'SHOP1'
					  AND g.GroupLanguageId = 'LANG1'
					ORDER BY g.GroupName

				"))
				{
					while (dataReader.Read())
					{
						editor.Options.Add(new()
						{
							Value = Converter.ToString(dataReader["GroupName"]),
							Label = Converter.ToString(dataReader["GroupName"])
						});
					}
				}
			return editor;
		}		
		private static Select CreateGroupIdSelect()
		{
			var editor = new Select
			{
				ShowNothingSelectedOption = true,
				//ReloadOnChange = true,
				//Readonly = true
			};
			var addedGroupIds = new HashSet<string>();
			using (IDataReader dataReader = Database.CreateDataReader(@"
					SELECT DISTINCT g.GroupId, g.GroupName
					FROM EcomGroups g
					INNER JOIN EcomShopGroupRelation sgr ON g.GroupId = sgr.ShopGroupGroupId
					WHERE sgr.ShopGroupShopId = 'SHOP1'
					  AND g.GroupLanguageId = 'LANG1'
					ORDER BY g.GroupName

				"))
			{
				while (dataReader.Read())
				{
					editor.Options.Add(new()
					{
						Value = Converter.ToString(dataReader["GroupId"]),
						Label = Converter.ToString(dataReader["GroupId"])
					});
				}
			}
			return editor;
		}
		protected override IEnumerable<EditScreenBase<MapCategoryPresetDataModel>.EditorMapping>? GetEditorMappings()
		{
			return new EditScreenBase<MapCategoryPresetDataModel>.EditorMapping[]
			{
				this.CreateMapping<string>(m => m.DWGroupId) with
				{
					ReadOnlyPredicate = _ => true // Always read-only
				}
			};
		}
		//protected override IEnumerable<EditScreenBase<MapCategoryPresetDataModel>.EditorMapping>? GetEditorMappings()
		//{
		//	return (IEnumerable<EditScreenBase<MapCategoryPresetDataModel>.EditorMapping>)new EditScreenBase<MapCategoryPresetDataModel>.EditorMapping[1]
		//	{
		//		this.CreateMapping<string>((Expression<Func<MapCategoryPresetDataModel, string>>) (m => m.DWGroupId)) with
		//		{
		//			ReadOnlyPredicate = (Func<MapCategoryPresetDataModel, bool>) (m => m.DWGroupId == "" ? true : true)
		//		}
		//	};
		//}
		protected override string GetScreenName() => "Edit Store Location";

		protected override CommandBase<MapCategoryPresetDataModel> GetSaveCommand() => new SaveMapCategory();
	}
}
