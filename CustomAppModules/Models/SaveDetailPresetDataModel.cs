using System;
using System.Collections.Generic;
using Dynamicweb.CoreUI.Data;
using Dynamicweb.CoreUI.Data.Validation;

namespace CustomAppModules.Models
{
	public sealed class SaveDetailPresetDataModel : DataViewModelBase, IIdentifiable
	{
		public int Id { get; set; }

		[ConfigurableProperty("First Name", null, null, null, true, false, false)]
		public string FirstName { get; set; } = "";
		[ConfigurableProperty("Last Name", null, null, null, true, false, false)]
		public string LastName { get; set; } = "";
		[ConfigurableProperty("Age", null, null, null, true, false, false)]
		public int Age { get; set; }

		public string GetId() => $"{Id}";
	}
}
