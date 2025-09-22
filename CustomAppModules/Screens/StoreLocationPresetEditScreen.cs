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
	public sealed class StoreLocationPresetEditScreen : EditScreenBase<StoreLocationPresetDataModel>
	{
		protected override void BuildEditScreen()
		{
			AddComponents("Store Location", new LayoutWrapper[]
			{
				new("Store Location", new[]
				{
					EditorFor(m => m.storeName),
					EditorFor(m => m.Address),
					EditorFor(m => m.City),
					EditorFor(m => m.Zipcode),
					EditorFor(m => m.Country),
					EditorFor(m => m.PhoneNo),
					EditorFor(m => m.Fax),
					EditorFor(m => m.Openinghours),
					EditorFor(m => m.Activate),
                    EditorFor(m => m.SortId),
                    EditorFor(m => m.StoreContactemail)
				})
			});
		}

		protected override EditorBase? GetEditor(string property)
		{
			EditorBase editor;
			switch (property)
			{
				case "Address":
					editor = (EditorBase)new Textarea();
					break;
				case "storeName":
					editor = (EditorBase)CreateStoreNameSelect();
					break;
				case "Openinghours":
					editor = (EditorBase)new Textarea();
					break;
				default:
					editor = (EditorBase)null;
					break;
			}
			return editor;
		}

		protected override IEnumerable<EditScreenBase<StoreLocationPresetDataModel>.EditorMapping>? GetEditorMappings()
		{
			return (IEnumerable<EditScreenBase<StoreLocationPresetDataModel>.EditorMapping>)new EditScreenBase<StoreLocationPresetDataModel>.EditorMapping[1]
			{
				this.CreateMapping<string>((Expression<Func<StoreLocationPresetDataModel, string>>) (m => m.storeName)) with
				{
					ReadOnlyPredicate = (Func<StoreLocationPresetDataModel, bool>) (m => m.storeName == "" ? false : true)
				}
			};
		}

		private static Select CreateStoreNameSelect()
		{
			var editor = new Select
			{
				ShowNothingSelectedOption = true,
				ReloadOnChange = true
			};
			using (IDataReader dataReader = Database.CreateDataReader("SELECT DISTINCT StockLocationName FROM EcomStockLocation WHERE StockLocationId NOT IN (SELECT StockLocationId FROM DemoStoreLocations)"))
			{
				while (dataReader.Read())
				{
					editor.Options.Add(new()
					{
						Value = Converter.ToString(dataReader["StockLocationName"]),
						Label = Converter.ToString(dataReader["StockLocationName"])
					});
				}
			}
			return editor;
		}

		protected override string GetScreenName() => "Edit Store Location";

		protected override CommandBase<StoreLocationPresetDataModel> GetSaveCommand() => new SaveStoreLocationPresetCommand();
	}
}
