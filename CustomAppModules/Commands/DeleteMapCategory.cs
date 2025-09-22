using CustomAppModules.Api;
using Dynamicweb.CoreUI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAppModules.Commands
{
	public sealed class DeleteMapCategory : CommandBase
	{
		public int Id { get; set; }

		public override CommandResult Handle() => MapCategoryPresetService.Delete(Id)
			? new() { Status = CommandResult.ResultType.Ok }
			: new() { Status = CommandResult.ResultType.Error, Message = "An unknown error occurred while trying to delete the data" };
	}
}
