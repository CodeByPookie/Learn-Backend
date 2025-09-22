using CustomWebApi.Models;
using Dynamicweb.Data;
using Dynamicweb.Ecommerce.International;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomWebApi.Services
{
	public class StudentsPresetService
	{
		public List<StudentsPresetModel> GetStudentsList()
		{
			var students = new List<StudentsPresetModel>();
			string sqlCommand = "SELECT * FROM Students";

			using (var reader = Database.CreateDataReader(sqlCommand))
			{
				while (reader.Read())
				{
					students.Add(new StudentsPresetModel
					{
						Id = reader["Id"] is DBNull ? 0 : Convert.ToInt32(reader["Id"]),
						StudentName = reader["StudentName"] is DBNull ? "" : reader["StudentName"].ToString(),
						StudentClass = reader["StudentClass"] is DBNull ? "" : reader["StudentClass"].ToString(),
						StudentAge = reader["StudentAge"] is DBNull ? 0 : Convert.ToInt32(reader["StudentAge"]),
						StudentEmail = reader["StudentEmail"] is DBNull ? "" : reader["StudentEmail"].ToString(),
						StudentPhone = reader["StudentPhone"] is DBNull ? "" : reader["StudentPhone"].ToString(),
						StudentAddress = reader["StudentAddress"] is DBNull ? "" : reader["StudentAddress"].ToString()
					});
				}
			}

			return students;
		}
		public StudentsPresetResponseStatus InsertStudentData(StudentsPresetModel data)
		{
			StudentsPresetResponseStatus responseStatus = new StudentsPresetResponseStatus();
			responseStatus.HttpStatusCode = System.Net.HttpStatusCode.OK;

			try
			{
				string sqlCommand = $@"INSERT INTO Students (StudentName, StudentClass, StudentAge, StudentEmail, StudentPhone, StudentAddress) 
									VALUES ('{data.StudentName}','{data.StudentClass}','{data.StudentAge}','{data.StudentEmail}','{data.StudentPhone}','{data.StudentAddress}')";
				Database.ExecuteNonQuery(sqlCommand);

			}
			catch (Exception ex)
			{
				responseStatus.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
				responseStatus.HttpStatusMessage = ex.Message;
			}
			return responseStatus;
		}
		public StudentsPresetResponseStatus UpdateStudentData(StudentsPresetModel data)
		{
			StudentsPresetResponseStatus responseStatus = new StudentsPresetResponseStatus();
			responseStatus.HttpStatusCode = System.Net.HttpStatusCode.OK;

			try
			{
				string sqlCommand = $@"UPDATE Students SET StudentName = '{data.StudentName}' , StudentClass = '{data.StudentClass}', StudentAge = '{data.StudentAge}', StudentEmail='{data.StudentEmail}', StudentPhone='{data.StudentPhone}', StudentAddress = '{data.StudentAddress}' WHERE Id = {data.Id}";
				Database.ExecuteNonQuery(sqlCommand);

			}
			catch (Exception ex)
			{
				responseStatus.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
				responseStatus.HttpStatusMessage = ex.Message;
			}
			return responseStatus;
		}
		public StudentsPresetResponseStatus DeleteStudentData(int id)
		{
			StudentsPresetResponseStatus responseStatus = new StudentsPresetResponseStatus();
			responseStatus.HttpStatusCode = System.Net.HttpStatusCode.OK;

			try
			{
				string sqlCommand = $@"DELETE from Students where Id='{id}' ";
				Database.ExecuteNonQuery(sqlCommand);
			}
			catch (Exception ex)
			{
				responseStatus.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
				responseStatus.HttpStatusMessage = ex.Message;
			}
			return responseStatus;
		}

	}
}
