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
	public sealed class SaveDetailPresetEditScreen : EditScreenBase<SaveDetailPresetDataModel>
	{
		protected override void BuildEditScreen()
		{
			AddComponents("User Detail", new LayoutWrapper[]
			{
				new("User Detail", new[]
				{
					EditorFor(m => m.FirstName),
					EditorFor(m => m.LastName),
					EditorFor(m => m.Age)
				})
			});
		}
		protected override EditorBase? GetEditor(string property)
		{
			EditorBase editor;
			switch (property)
			{
				case "FirstName":
				case "LastName":				
					editor = (EditorBase)new Textarea();
					break;
				default:
					editor = (EditorBase)null;
					break;
			}
			return editor;
		}
		protected override string GetScreenName() => "Edit User Detail";

		protected override CommandBase<SaveDetailPresetDataModel> GetSaveCommand() => new AddSaveDetail();

	}
}
