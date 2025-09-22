using Dynamicweb.CoreUI.Editors.Lists;
using Dynamicweb.Data;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace CustomAppModules.Api
{
	internal static class MapCategoryPresetService
	{
		public static MapCategoryPreset? GetMapCategoryById(int id)
		{
			return GetEntity(CommandBuilder.Create("SELECT * FROM [DemoCategoryMap] WHERE [Id] = {0}", id));
		}

		public static IEnumerable<MapCategoryPreset> GetMapCategory()
		{
			return GetEntities(CommandBuilder.Create("SELECT * FROM [DemoCategoryMap]"));
			
		}
		public static bool Save(MapCategoryPreset preset)
		{
			var sql = new CommandBuilder();

			sql.Add("MERGE DemoCategoryMap WITH (SERIALIZABLE) AS T");
			sql.Add("USING (VALUES ");
			sql.Add("   ({0},{1},{2},{3})", preset.Id, preset.DWGroupName, preset.DWGroupId, preset.DemoGroupId);
			sql.Add(") AS S (Id, DWGroupName, DWGroupId, DemoGroupId)");
			sql.Add("   ON S.Id = T.Id");
			sql.Add("WHEN MATCHED THEN");
			sql.Add("   UPDATE SET T.DWGroupName = S.DWGroupName, T.DWGroupId = S.DWGroupId, T.DemoGroupId = S.DemoGroupId");
			sql.Add("WHEN NOT MATCHED THEN");
			sql.Add("   INSERT (DWGroupName, DWGroupId, DemoGroupId) VALUES (S.DWGroupName, S.DWGroupId, S.DemoGroupId) ");
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
				return Database.ExecuteNonQuery(CommandBuilder.Create("DELETE FROM [DemoCategoryMap] WHERE Id={0}", Id)) > 0;
			}
			catch
			{
				return false;
			}
		}
		public static MapCategoryPreset? GetEntity(CommandBuilder cb)
		{
			var reader = Database.CreateDataReader(cb);
			if (reader.Read())
				return MapData(reader);
			return null;
		}
		private static IEnumerable<MapCategoryPreset> GetEntities(CommandBuilder cb)
		{
			var reader = Database.CreateDataReader(cb);
			while (reader.Read())
			{
				yield return MapData(reader);
			}
		}
		public static MapCategoryPreset MapData(IDataReader reader)
		{
			//string? dwGroupId = "";
			//string groupName = reader["DWGroupName"] is DBNull ? "" : reader["DWGroupName"].ToString();
			//string shopId = "SHOP1"; 

			//var getGroupNameDataSql = CommandBuilder.Create(@"
			//		SELECT DISTINCT g.GroupId
			//		FROM EcomGroups g
			//		INNER JOIN EcomShopGroupRelation sgr ON g.GroupId = sgr.ShopGroupGroupId
			//		WHERE g.GroupName = {0} AND sgr.ShopGroupShopId = {1}
			//	", groupName, shopId);
			////string getGroupNameDataSql = @"SELECT DISTINCT GroupID FROM EcomGroups where GroupName ='" + reader["DWGroupName"].ToString() + "'";
			//using (IDataReader getGroupNameData = Database.CreateDataReader(getGroupNameDataSql))
			//{
			//	while (getGroupNameData.Read())
			//	{
			//		dwGroupId = getGroupNameData["GroupId"] is DBNull ? "" : getGroupNameData["GroupId"].ToString();
			//	}
			//}

			return new()
			{
				Id = reader.GetInt32(reader.GetOrdinal("Id")),
				DWGroupId = reader["DWGroupId"] is DBNull ? "" : reader["DWGroupId"].ToString(),
				DWGroupName = reader["DWGroupName"] is DBNull ? "" : reader["DWGroupName"].ToString(),
				DemoGroupId = reader["DemoGroupId"] is DBNull ? "" : reader["DemoGroupId"].ToString(),
			};
		}
	}
}
