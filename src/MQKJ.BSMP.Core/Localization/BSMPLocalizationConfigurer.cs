using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace MQKJ.BSMP.Localization
{
    public static class BSMPLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(BSMPConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(BSMPLocalizationConfigurer).GetAssembly(),
                        "MQKJ.BSMP.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
