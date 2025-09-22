
using Dynamicweb.Data;
using System.Data;

namespace CustomAppModules.Api
{
	internal static class SaveDetailPresetService
	{
		public static SaveDetailPreset? GetSaveDetailById(int id)
		{
			return GetEntity(CommandBuilder.Create("SELECT * FROM [DemoSaveDetail] WHERE [Id] = {0}", id));
		}
		public static IEnumerable<SaveDetailPreset> GetSaveDataPresets()
		{
			return GetEntities(CommandBuilder.Create("SELECT * FROM [DemoSaveDetail]"));
		}
		public static bool Save(SaveDetailPreset preset)
		{
			var sql = new CommandBuilder();

			sql.Add("MERGE DemoSaveDetail WITH (SERIALIZABLE) AS T");
			sql.Add("USING (VALUES ");
			sql.Add("   ({0},{1},{2},{3})", preset.Id, preset.FirstName, preset.LastName, preset.Age);
			sql.Add(") AS S (Id, FirstName, LastName, Age)");
			sql.Add("   ON S.Id = T.Id");
			sql.Add("WHEN MATCHED THEN");
			sql.Add("   UPDATE SET T.FirstName = S.FirstName,T.LastName = S.LastName,T.Age = S.Age");
			sql.Add("WHEN NOT MATCHED THEN");
			sql.Add("   INSERT (FirstName, LastName, Age) VALUES (S.FirstName, S.LastName, S.Age) ");
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
				return Database.ExecuteNonQuery(CommandBuilder.Create("DELETE FROM [DemoSaveDetail] WHERE Id={0}", Id)) > 0;
			}
			catch
			{
				return false;
			}
		}
		
		private static SaveDetailPreset? GetEntity(CommandBuilder cb)
		{
			var reader = Database.CreateDataReader(cb);
			if (reader.Read())
				return MapData(reader);
			return null;
		}

		private static IEnumerable<SaveDetailPreset> GetEntities(CommandBuilder cb)
		{
			var reader = Database.CreateDataReader(cb);
			while (reader.Read())
			{
				yield return MapData(reader);
			}
		}

		private static SaveDetailPreset MapData(IDataReader reader)
		{	
			return new()
			{
				Id = reader.GetInt32(reader.GetOrdinal("Id")),
				FirstName = reader["FirstName"] is DBNull ? "" : reader["FirstName"].ToString(),
				LastName = reader["LastName"] is DBNull ? "" : reader["LastName"].ToString(),				
				Age = reader.GetInt32(reader.GetOrdinal("Age"))
			};
		}
	}
}
