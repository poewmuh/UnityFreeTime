using GameFolder.Data;
using GameFolder.Localization;

namespace GameFolder.Boot
{
    public class AssetDataLoader : DataLoaderBase
    {
        public override void Load()
        {
            var languageType = ProfileData.GetLocalData<ApplicationSettingsData>().LanguageType;
            LocalizationLoader.LoadLocalization(languageType);
        }

        public override void UnLoad()
        {
            LocalizationLoader.UnloadLocalization();
        }
    }
}