using CustomAppModules.Commands;
using CustomAppModules.Models;
using Dynamicweb.CoreUI.Data;
using Dynamicweb.CoreUI.Editors.Inputs;
using Dynamicweb.CoreUI.Editors.Lists;
using Dynamicweb.CoreUI.Editors;
using Dynamicweb.CoreUI.Screens;

namespace CustomAppModules.Screens
{
	public sealed class StudentsPresetEditScreen : EditScreenBase<StudentsPresetDataModel>
	{
		protected override void BuildEditScreen()
		{
			AddComponents("Student Detail", new LayoutWrapper[]
			{
				new("Student Detail", new[]
				{
					EditorFor(m => m.StudentName),
					EditorFor(m => m.StudentClass),
					EditorFor(m => m.StudentAge),
					EditorFor(m => m.StudentEmail),
					EditorFor(m => m.StudentPhone),
					EditorFor(m => m.StudentAddress)
				})
			});
		}
		protected override EditorBase? GetEditor(string property)
		{
			EditorBase editor;
			switch (property)
			{				
				case "StudentAddress":
					editor = (EditorBase)new Textarea();
					break;
				case "StudentClass":
					editor = (EditorBase)CreateStudentClassSelect();
					break;				
				default:
					editor = (EditorBase)null;
					break;
			}
			return editor;
		}
		private static Select CreateStudentClassSelect()
		{
			var editor = new Select
			{
				ShowNothingSelectedOption = true,
				ReloadOnChange = true
			};
			editor.Options.Add(new()
			{
				Value = "Sec A",
				Label = "Sec A"
			});
			editor.Options.Add(new()
			{
				Value = "Sec B",
				Label = "Sec B"
			});
			editor.Options.Add(new()
			{
				Value = "Sec C",
				Label = "Sec C"
			});
			editor.Options.Add(new()
			{
				Value = "Sec D",
				Label = "Sec D"
			});
			editor.Options.Add(new()
			{
				Value = "Sec E",
				Label = "Sec E"
			});
			return editor;
		}
		protected override string GetScreenName() => "Edit Student Detail";
		protected override CommandBase<StudentsPresetDataModel> GetSaveCommand() => new SaveStudentsPresetCommand();
	}
}
