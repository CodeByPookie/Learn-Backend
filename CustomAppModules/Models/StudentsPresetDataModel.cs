using Dynamicweb.CoreUI.Data;
using Dynamicweb.CoreUI.Data.Validation;
using System.ComponentModel;

namespace CustomAppModules.Models
{
	public sealed class StudentsPresetDataModel : DataViewModelBase, IIdentifiable
	{
		public int Id { get; set; }
	
		[ConfigurableProperty("Student Name", null, null, null, true, false, false)]
		[Required(ErrorMessage = "Please fill the Student Name")]
		public string StudentName { get; set; } = "";

		[ConfigurableProperty("Student Age", null, null, null, true, false, false)]
		[Required(ErrorMessage = "Please fill the Student Age")]
		public int StudentAge { get; set; } = 0;

		[ConfigurableProperty("Student Class", null, null, null, true, false, false)]
		[Required(ErrorMessage = "Please fill the Student Class")]
		public string StudentClass { get; set; } = "";

		[ConfigurableProperty("Student Email", null, null, null, true, false, false)]
		[Required(ErrorMessage = "Please fill the Student Email")]
		public string StudentEmail { get; set; } = "";

		[ConfigurableProperty("Student Phone", null, null, null, true, false, false)]
		[Required(ErrorMessage = "Please fill the Student Phone")]
		public string StudentPhone { get; set; } = "";

		[ConfigurableProperty("Student Address", null, null, null, true, false, false)]
		[Required(ErrorMessage = "Please fill the Student Address")]
		public string StudentAddress { get; set; } = "";

		public string GetId() => $"{Id}";
	}
}
