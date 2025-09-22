using Microsoft.AspNetCore.Mvc;
using CustomWebApi.Models;
using CustomWebApi.Services;
using Dynamicweb.Content.Commenting;


namespace CustomWebApi.Controllers
{
	[Route("custom/[controller]")]
	[ApiController]
	public class StudentPresetController : ControllerBase
	{
		[HttpGet("get")]
		public IActionResult GetStudents()
		{
			StudentsPresetService service = new StudentsPresetService();

			var students = service.GetStudentsList();
			return Ok(new { data = students });
		}

		[HttpPost("insert")]
		public IActionResult InsertStudent([FromBody] StudentsPresetModel data)
		{
			StudentsPresetService service = new StudentsPresetService();
			var res = service.InsertStudentData(data);
			return Ok(res);
		}

		[HttpPost("update")]
		public IActionResult UpdateStudent([FromBody] StudentsPresetModel data)
		{
			StudentsPresetService service = new StudentsPresetService();
			var res = service.UpdateStudentData(data);
			return Ok(res);
		}

		[HttpPost("delete")]
		public IActionResult DeleteStudent([FromBody] int id)
		{
			StudentsPresetService service = new StudentsPresetService();
			var res = service.DeleteStudentData(id);
			return Ok(res);
		}

	}
}
