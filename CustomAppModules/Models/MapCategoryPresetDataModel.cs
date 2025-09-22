using Dynamicweb.CoreUI.Data;
using Dynamicweb.CoreUI.Data.Validation;
using System.ComponentModel;

namespace CustomAppModules.Models
{
	public sealed class MapCategoryPresetDataModel : DataViewModelBase, IIdentifiable
	{
		public int Id { get; set; }

		[ConfigurableProperty("DWGroupName", null, null, null, true, false, false)]
		[Required(ErrorMessage = "Please select DWGroupName")]
		public string DWGroupName { get; set; } = "";
		
		[ConfigurableProperty("DWGroupId", null, null, null, true, false, false)]
		//[ReadOnly(true)]
		public string DWGroupId { get; set; } = "";

		[ConfigurableProperty("DemoGroupId", null, null, null, true, false, false)]
		[Required(ErrorMessage = "Please fill DWGroupName")]
		public string DemoGroupId { get; set; } = "";

		public string GetId() => $"{Id}";
	}
}
