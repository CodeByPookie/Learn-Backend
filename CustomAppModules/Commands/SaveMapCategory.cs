using CustomAppModules.Api;
using CustomAppModules.Models;
using Dynamicweb.CoreUI.Data;
using System.Data;


namespace CustomAppModules.Commands
{
	public sealed class SaveMapCategory : CommandBase<MapCategoryPresetDataModel>
	{
		public override CommandResult Handle()
		{
			if (Model is null) return new()
			{
				Status = CommandResult.ResultType.Invalid,
				Message = "Model data must be given"
			};

			var isNew = Model.Id == 0;
			var preset = isNew ? new() : MapCategoryPresetService.GetMapCategoryById(Model!.Id) ?? new();

			string dwGroupId = "";
			string getdwGroupIdSql = @"
					SELECT g.GroupId 
					FROM EcomGroups g
					INNER JOIN EcomShopGroupRelation sgr ON sgr.ShopGroupGroupId = g.GroupId
					WHERE g.GroupName = '" + Model.DWGroupName + @"'
					AND sgr.ShopGroupShopId = 'SHOP1'";
			using (IDataReader getGroupIdId = Dynamicweb.Data.Database.CreateDataReader(getdwGroupIdSql))
			{
				while (getGroupIdId.Read())
				{
					dwGroupId = getGroupIdId["GroupId"] is DBNull ? "" : getGroupIdId["GroupId"].ToString();
				}
			}
			
			preset.DWGroupId = dwGroupId;
			preset.DWGroupName = Model.DWGroupName;
			preset.DemoGroupId = Model.DemoGroupId;

			if (!MapCategoryPresetService.Save(preset)) return new()
			{
				Status = CommandResult.ResultType.Error,
				Message = "An unknown error occurred while saving the preset"
			};

			return new()
			{
				Status = CommandResult.ResultType.Ok,
				Model = Model
			};
		}
	}
}
