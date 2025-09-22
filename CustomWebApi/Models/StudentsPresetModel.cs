using System.Net;

namespace CustomWebApi.Models
{
	public class StudentsPresetModel
	{
		public int Id { get; set; }
		public string StudentName { get; set; }
		public string StudentClass { get; set; }
		public int StudentAge { get; set; }
		public string StudentEmail { get; set; }
		public string StudentPhone { get; set; }
		public string StudentAddress { get; set; }
	}
	public class StudentsPresetResponseStatus
	{
		public HttpStatusCode HttpStatusCode { get; set; }
		public string HttpStatusMessage { get; set; }
	}

}
