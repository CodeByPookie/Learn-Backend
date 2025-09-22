using Dynamicweb.CoreUI.Actions.Implementations;
using Dynamicweb.CoreUI.Data;
using Dynamicweb.CoreUI.Navigation;
using Dynamicweb.CoreUI.Icons;
using CustomAppModules.Queries;
using CustomAppModules.Screens;

namespace CustomAppModules.Tree
{
	public sealed class ConfiguratorsNodeProvider : NavigationNodeProvider<ConfiguratorsSection>
	{
		internal static string RootId = "StoreLocation";

		public override IEnumerable<NavigationNode> GetRootNodes()
		{
			return new List<NavigationNode>()
			{				
				CreateNode("Store Location", RootId, Icon.Apps, 30, false),
				CreateMapCategoryNode("Map Category", RootId, Icon.Apps, 30, false),
				CreateTestNode("User Detail", RootId, Icon.Apps, 30, false),
				CreateStudentsNode("Students Detail", RootId, Icon.Apps, 30, false)
            };
		}

		public override IEnumerable<NavigationNode> GetSubNodes(NavigationNodePath parentNodePath)
		{
			yield break;
		}

		private static NavigationNode CreateNode(string name, string id, Icon icon, int sort, bool hasSubNodes)
		{
			NavigationNode node = new NavigationNode();
			node.Name = name;
			node.Id = id;
			node.Icon = icon;
			node.Sort = sort;
			node.HasSubNodes = hasSubNodes;
			node.ContextActions.Add(CommonActionsHelper.GetPermissionActionNode());
			node.NodeAction = NavigateScreenAction.To<StoreLocationPresetListScreen>().With((DataQueryBase)new StoreLocationQuery());
			return node;
		}
		private static NavigationNode CreateMapCategoryNode(string name, string id, Icon icon, int sort, bool hasSubNodes)
		{
			NavigationNode node = new NavigationNode();
			node.Name = name;
			node.Id = id;
			node.Icon = icon;
			node.Sort = sort;
			node.HasSubNodes = hasSubNodes;
			node.ContextActions.Add(CommonActionsHelper.GetPermissionActionNode());
			node.NodeAction = NavigateScreenAction.To<MapCategoryPresetListScreen>().With((DataQueryBase)new MapCategoryQuery());
			return node;
		}
		private static NavigationNode CreateTestNode(string name, string id, Icon icon, int sort, bool hasSubNodes)
		{
			NavigationNode node = new NavigationNode();
			node.Name = name;
			node.Id = id;
			node.Icon = icon;
			node.Sort = sort;
			node.HasSubNodes = hasSubNodes;
			node.ContextActions.Add(CommonActionsHelper.GetPermissionActionNode());
			node.NodeAction = NavigateScreenAction.To<SaveDetailPresetListScreen>().With((DataQueryBase)new SaveDetailQuery());
			return node;
		}
		private static NavigationNode CreateStudentsNode(string name, string id, Icon icon, int sort, bool hasSubNodes)
		{
			NavigationNode node = new NavigationNode();
			node.Name = name;
			node.Id = id;
			node.Icon = icon;
			node.Sort = sort;
			node.HasSubNodes = hasSubNodes;
			node.ContextActions.Add(CommonActionsHelper.GetPermissionActionNode());
			node.NodeAction = NavigateScreenAction.To<StudentsPresetListScreen>().With((DataQueryBase)new StudentsQuery());
			return node;
		}

    }
}
