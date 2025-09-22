using Dynamicweb.CoreUI.Actions.Implementations;
using Dynamicweb.CoreUI.Actions;
using Dynamicweb.CoreUI.Icons;

namespace CustomAppModules
{
	internal class CommonActionsHelper
	{
		internal static ActionNode GetPermissionActionNode() => new ActionNode()
		{
			Name = "Permissions",
			Icon = Icon.Lock,
			Sort = int.MaxValue,
			NodeAction = (ActionBase)ShowMessageAction.WithMessage("Permissions dialog will be here soon")
		};
	}
}
