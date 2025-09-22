using CustomAppModules.Models;
using Dynamicweb.CoreUI.Navigation;

namespace CustomAppModules.Tree
{
	public sealed class StoreLocationNavigationNodePathProvider : NavigationNodePathProvider<StoreLocationPresetDataModel>
	{
		internal static readonly IEnumerable<string> PathRootSegments = (IEnumerable<string>)new string[4]
		{
			typeof (CustomAppArea).FullName ?? "",
			NavigationContext.Empty,
			typeof (ConfiguratorsSection).FullName ?? "",
			"StoreLocation"
		};

		protected override NavigationNodePath? GetNavigationNodePathInternal(StoreLocationPresetDataModel? model)
		{
			return new NavigationNodePath(StoreLocationNavigationNodePathProvider.PathRootSegments);
		}
	}
}
