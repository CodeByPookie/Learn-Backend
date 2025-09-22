namespace CustomAppModules.Api
{
	public sealed class StoreLocationPreset
	{
		public int Id { get; set; }
		public int stocklocationId { get; set; }
		public string storeName { get; set; } = "";
		public string Address { get; set; } = "";
		public string City { get; set; } = "";
		public string Zipcode { get; set; } = "";
		public string Country { get; set; } = "";
		public string PhoneNo { get; set; } = "";
		public string Fax { get; set; } = "";
		public string Openinghours { get; set; } = "";
		public bool Activate { get; set; }
		public int SortId { get; set; } = 0;
        public string StoreContactemail { get; set; } = "";
	}
}
