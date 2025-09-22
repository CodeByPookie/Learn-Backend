using Dynamicweb.Data;
using System.Data;

namespace CustomAppModules.Api
{
	internal static class StoreLocationPresetService
	{
		public static StoreLocationPreset? GetStoreLocationById(int id)
		{
			return GetEntity(CommandBuilder.Create("SELECT * FROM [DemoStoreLocations] WHERE [Id] = {0}", id));
		}

		public static IEnumerable<StoreLocationPreset> GetStoreLocations(bool onlyActivestores = false)
		{
			if (onlyActivestores)
			{
				return GetEntities(CommandBuilder.Create("SELECT * FROM [DemoStoreLocations] WHERE Activate='true'"));
			}
			else
			{
				return GetEntities(CommandBuilder.Create("SELECT * FROM [DemoStoreLocations]"));
			}
		}

		public static bool Save(StoreLocationPreset preset)
		{
			var sql = new CommandBuilder();

			sql.Add("MERGE DemoStoreLocations WITH (SERIALIZABLE) AS T");
			sql.Add("USING (VALUES ");
			sql.Add("   ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11})", preset.Id, preset.stocklocationId, preset.Address, preset.City, preset.Zipcode, preset.Country, preset.PhoneNo, preset.Fax, preset.Openinghours, preset.SortId, preset.Activate, preset.StoreContactemail);
			sql.Add(") AS S (Id, StockLocationId, Address, City, ZipCode, Country, PhoneNo, Fax, OpeningHours, SortId, Activate, StoreContactEmail)");
			sql.Add("   ON S.Id = T.Id");
			sql.Add("WHEN MATCHED THEN");
			sql.Add("   UPDATE SET T.StockLocationId = S.StockLocationId, T.Address = S.Address, T.City = S.City, T.ZipCode = S.ZipCode, T.Country = S.Country,T.PhoneNo = S.PhoneNo,T.Fax = S.Fax,T.OpeningHours = S.OpeningHours,T.SortId = S.SortId,T.Activate = S.Activate,T.StoreContactEmail = S.StoreContactEmail ");
			sql.Add("WHEN NOT MATCHED THEN");
			sql.Add("   INSERT (StockLocationId, Address, City, ZipCode, Country, PhoneNo, Fax, OpeningHours, SortId, Activate, StoreContactEmail) VALUES (S.StockLocationId, S.Address, S.City, S.ZipCode, S.Country, S.PhoneNo, S.Fax, S.OpeningHours, S.SortId, S.Activate, S.StoreContactEmail) ");
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
				return Database.ExecuteNonQuery(CommandBuilder.Create("DELETE FROM [DemoStoreLocations] WHERE Id={0}", Id)) > 0;
			}
			catch
			{
				return false;
			}
		}

		private static StoreLocationPreset? GetEntity(CommandBuilder cb)
		{
			var reader = Database.CreateDataReader(cb);
			if (reader.Read())
				return MapData(reader);
			return null;
		}

		private static IEnumerable<StoreLocationPreset> GetEntities(CommandBuilder cb)
		{
			var reader = Database.CreateDataReader(cb);
			while (reader.Read())
			{
				yield return MapData(reader);
			}
		}

		private static StoreLocationPreset MapData(IDataReader reader)
		{
			string? StoreName = "";
			string getStoreNameDataSql = @"SELECT StockLocationName FROM EcomStockLocation where StockLocationId ='" + reader.GetInt32(reader.GetOrdinal("stocklocationId")) + "'";
			using (IDataReader getStoreNameData = Database.CreateDataReader(getStoreNameDataSql))
			{
				while (getStoreNameData.Read())
				{
					StoreName = getStoreNameData["StockLocationName"] is DBNull ? "" : getStoreNameData["StockLocationName"].ToString();
				}
			}

			return new()
			{
				Id = reader.GetInt32(reader.GetOrdinal("Id")),
				stocklocationId = reader.GetInt32(reader.GetOrdinal("stocklocationId")),
				storeName = StoreName,
				Address = reader["Address"] is DBNull ? "" : reader["Address"].ToString(),
				City = reader["City"] is DBNull ? "" : reader["City"].ToString(),
				Zipcode = reader["Zipcode"] is DBNull ? "" : reader["Zipcode"].ToString(),
				Country = reader["Country"] is DBNull ? "" : reader["Country"].ToString(),
				PhoneNo = reader["PhoneNo"] is DBNull ? "" : reader["PhoneNo"].ToString(),
				Fax = reader["Fax"] is DBNull ? "" : reader["Fax"].ToString(),
				Openinghours = reader["Openinghours"] is DBNull ? "" : reader["Openinghours"].ToString(),
				Activate = reader.GetBoolean(reader.GetOrdinal("Activate")),
                SortId = reader.GetInt32(reader.GetOrdinal("SortId")),
                StoreContactemail = reader["StoreContactemail"] is DBNull ? "" : reader["StoreContactemail"].ToString()
			};
		}
	}
}
