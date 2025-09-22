using Dynamicweb.CoreUI.Navigation;

namespace CustomAppModules.Tree
{
	public sealed class ConfiguratorsSection : NavigationSection<CustomAppArea>
	{
		public ConfiguratorsSection(NavigationContext context) : base(context)
		{
			Name = "Configurators";
			Sort = 30;
			ContextActions.Add(CommonActionsHelper.GetPermissionActionNode());
		}
	}
}
