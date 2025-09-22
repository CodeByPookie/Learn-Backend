namespace CustomAppModules.Api
{
	public sealed class StudentsPreset
	{
		public int Id { get; set; }
		public string StudentName { get; set; } = "";
		public string StudentClass { get; set; } = "";
		public int StudentAge { get; set; } = 0;	
		public string StudentEmail { get; set; } = "";
		public string StudentPhone { get; set; } = "";
		public string StudentAddress { get; set; } = "";
	}
}
