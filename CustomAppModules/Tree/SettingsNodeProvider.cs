using Dynamicweb.Application.UI;
using Dynamicweb.CoreUI.Actions.Implementations;
using Dynamicweb.CoreUI.Navigation;
using CustomAppModules.Screens;
using CustomAppModules.Queries;
using Dynamicweb.CoreUI.Actions;
using Dynamicweb.CoreUI.Data;

namespace CustomAppModules.Tree
{
	public sealed class SettingsNodeProvider : NavigationNodeProvider<AreasSection>
	{
		public override IEnumerable<NavigationNode> GetRootNodes()
		{
			yield return new()
			{
				Id = "StoreLocation",
				Name = "Store Location",
				NodeAction = NavigateScreenAction.To<StoreLocationPresetListScreen>().With((DataQueryBase)new StoreLocationQuery()),
				HasSubNodes = false,
				Icon = Dynamicweb.CoreUI.Icons.Icon.Apps
			};
		}
		public override IEnumerable<NavigationNode> GetSubNodes(NavigationNodePath parentNodePath)
		{
			yield break;
		}
	}
}
