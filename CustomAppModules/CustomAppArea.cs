using Dynamicweb.CoreUI.Actions;
using Dynamicweb.CoreUI.Application;
using Dynamicweb.Application.UI.Queries;
using Dynamicweb.Application.UI.Screens;
using Dynamicweb.CoreUI.Actions.Implementations;
using Dynamicweb.CoreUI.Data;
using Dynamicweb.CoreUI.Icons;
using Dynamicweb.Security.Permissions;

namespace CustomAppModules
{
	public sealed class CustomAppArea : AreaBase
	{
		public static readonly string AreaName = "Custom Apps";
		public CustomAppArea()
		{
			Name = AreaName;
			Icon = Icon.Folder;
			Sort = 90;
			ContextActions.AddRange(GetContextActions());
		}

		private IEnumerable<ActionNode> GetContextActions()
		{
			PermissionSection permissionSection = GetPermissionSection();
			return new List<ActionNode>()
			{
				new ActionNode()
				{
					Name = "Permissions",
					Icon = Icon.Lock,
					NodeAction =  NavigateScreenAction.To<PermissionListScreen>().With( new PermissionsByIdentifierQuery()
					{
						Name = permissionSection.GetPermissionName(),
						Key = permissionSection.GetPermissionKey()
					}),
					PermissionLevelRequired = new PermissionLevel?(PermissionLevel.All)
				}
			}.WithPermission(permissionSection.GetPermission());
		}
	}
}
