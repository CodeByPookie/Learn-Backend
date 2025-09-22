using Dynamicweb.CoreUI.Data;
using Dynamicweb.CoreUI.Data.Validation;

namespace CustomAppModules.Models
{
	public sealed class StoreLocationPresetDataModel : DataViewModelBase, IIdentifiable
	{
		public int Id { get; set; }

		[ConfigurableProperty("Store Name", null, null, null, true, false, false)]
		[Required(ErrorMessage = "Please select store name")]
		public string storeName { get; set; } = "";

		[ConfigurableProperty("Address", null, null, null, true, false, false)]
		public string Address { get; set; } = "";

		[ConfigurableProperty("City", null, null, null, true, false, false)]
		public string City { get; set; } = "";

		[ConfigurableProperty("Zip Code", null, null, null, true, false, false)]
		public string Zipcode { get; set; } = "";

		[ConfigurableProperty("Country", null, null, null, true, false, false)]
		public string Country { get; set; } = "";

		[ConfigurableProperty("Phone No", null, null, null, true, false, false)]
		public string PhoneNo { get; set; } = "";

		[ConfigurableProperty("Fax", null, null, null, true, false, false)]
		public string Fax { get; set; } = "";

		[ConfigurableProperty("Opening Hours", null, null, null, true, false, false)]
		public string Openinghours { get; set; } = "";

		[ConfigurableProperty("Activate", null, null, null, true, false, false)]
		public bool Activate { get; set; }

        [ConfigurableProperty("SortId", null, null, null, true, false, false)]
        public int SortId { get; set; }

        [ConfigurableProperty("Store Contact Email", null, null, null, true, false, false)]
		public string StoreContactemail { get; set; } = "";

		public string GetId() => $"{Id}";
	}
}
