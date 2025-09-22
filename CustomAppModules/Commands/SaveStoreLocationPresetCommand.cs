using CustomAppModules.Api;
using CustomAppModules.Models;
using Dynamicweb.CoreUI.Data;
using System.Data;

namespace CustomAppModules.Commands
{
	public sealed class SaveStoreLocationPresetCommand : CommandBase<StoreLocationPresetDataModel>
	{
		public override CommandResult Handle()
		{
			if (Model is null) return new()
			{
				Status = CommandResult.ResultType.Invalid,
				Message = "Model data must be given"
			};

			var isNew = Model.Id == 0;
			var preset = isNew ? new() : StoreLocationPresetService.GetStoreLocationById(Model!.Id) ?? new();

			int StockLocationId = 0;
			string getStockLocationIdSql = @"SELECT StockLocationId FROM EcomStockLocation where StockLocationName ='" + Model.storeName + "'";
			using (IDataReader getStockLocationId = Dynamicweb.Data.Database.CreateDataReader(getStockLocationIdSql))
			{
				while (getStockLocationId.Read())
				{
					StockLocationId = getStockLocationId["StockLocationId"] is DBNull ? 0 : Convert.ToInt32(getStockLocationId["StockLocationId"].ToString());
				}
			}

			preset.stocklocationId = StockLocationId;
			preset.storeName = Model.storeName;
			preset.Address = Model.Address;
			preset.City = Model.City;
			preset.Zipcode = Model.Zipcode;
			preset.Country = Model.Country;
			preset.PhoneNo = Model.PhoneNo;
			preset.Fax = Model.Fax;
			preset.Openinghours = Model.Openinghours;
			preset.Activate = Model.Activate;
            preset.SortId = Model.SortId;
            preset.StoreContactemail = Model.StoreContactemail;

			if (!StoreLocationPresetService.Save(preset)) return new()
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
