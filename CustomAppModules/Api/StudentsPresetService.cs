using Dynamicweb.Data;
using System.Data;

namespace CustomAppModules.Api
{
	internal static class StudentsPresetService
	{
		public static StudentsPreset? GetStudentById(int id)
		{
			return GetEntity(CommandBuilder.Create("SELECT * FROM [Students] WHERE [Id] = {0}", id));
		}
		public static IEnumerable<StudentsPreset> GetStudentsPresets()
		{
			return GetEntities(CommandBuilder.Create("SELECT * FROM [Students]"));
		}
		public static bool Save(StudentsPreset preset)
		{
			var sql = new CommandBuilder();

			sql.Add("MERGE Students WITH (SERIALIZABLE) AS T");
			sql.Add("USING (VALUES ");
			sql.Add("   ({0},{1},{2},{3},{4},{5},{6})", preset.Id, preset.StudentName, preset.StudentClass, preset.StudentAge, preset.StudentEmail, preset.StudentPhone, preset.StudentAddress);
			sql.Add(") AS S (Id, StudentName, StudentClass, StudentAge, StudentEmail, StudentPhone, StudentAddress)");
			sql.Add("   ON S.Id = T.Id");
			sql.Add("WHEN MATCHED THEN");
			sql.Add("   UPDATE SET T.StudentName = S.StudentName,T.StudentClass = S.StudentClass,T.StudentAge = S.StudentAge,T.StudentEmail = S.StudentEmail,T.StudentPhone = S.StudentPhone,T.StudentAddress = S.StudentAddress ");
			sql.Add("WHEN NOT MATCHED THEN");
			sql.Add("   INSERT (StudentName, StudentClass, StudentAge, StudentEmail, StudentPhone, StudentAddress) VALUES (S.StudentName, S.StudentClass, S.StudentAge, S.StudentEmail, S.StudentPhone, S.StudentAddress) ");
			sql.Add("OUTPUT INSERTED.Id;");

			int identity = 0;
			try
			{
				identity = (int)Database.ExecuteScalar(sql);
				if (identity != preset.Id)
				{
					preset.Id = identity;
				}
			}
			catch (Exception e)
			{

			}
			return identity > 0;
		}
		public static bool Delete(int Id)
		{
			try
			{
				return Database.ExecuteNonQuery(CommandBuilder.Create("DELETE FROM [Students] WHERE Id={0}", Id)) > 0;
			}
			catch
			{
				return false;
			}
		}
		private static StudentsPreset? GetEntity(CommandBuilder cb)
		{
			var reader = Database.CreateDataReader(cb);
			if (reader.Read())
				return MapData(reader);
			return null;
		}
		private static IEnumerable<StudentsPreset> GetEntities(CommandBuilder cb)
		{
			var reader = Database.CreateDataReader(cb);
			while (reader.Read())
			{
				yield return MapData(reader);
			}
		}
		private static StudentsPreset MapData(IDataReader reader)
		{
			return new()
			{
				Id = reader.GetInt32(reader.GetOrdinal("Id")),
				StudentName = reader["StudentName"] is DBNull ? "" : reader["StudentName"].ToString(),
				StudentClass = reader["StudentClass"] is DBNull ? "" : reader["StudentClass"].ToString(),
				StudentAge = reader.GetInt32(reader.GetOrdinal("StudentAge")),
				StudentEmail = reader["StudentEmail"] is DBNull ? "" : reader["StudentEmail"].ToString(),
				StudentPhone = reader["StudentPhone"] is DBNull ? "" : reader["StudentPhone"].ToString(),
				StudentAddress = reader["StudentAddress"] is DBNull ? "" : reader["StudentAddress"].ToString()
			};
		}
	}
}
