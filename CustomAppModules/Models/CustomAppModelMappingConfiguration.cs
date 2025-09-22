using Dynamicweb.Extensibility.Mapping;
using CustomAppModules.Api;

namespace CustomAppModules.Models
{
	public sealed class CustomAppModelMappingConfiguration : MappingConfigurationBase
	{
		public CustomAppModelMappingConfiguration()
		{
			CreateMap<StoreLocationPreset, StoreLocationPresetDataModel>();
			CreateMap<MapCategoryPreset, MapCategoryPresetDataModel>();
			CreateMap<SaveDetailPreset, SaveDetailPresetDataModel>();
			CreateMap<StudentsPreset, StudentsPresetDataModel>();
		}
	}
}
