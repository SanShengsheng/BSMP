using System.Collections.Generic;
using Abp.Configuration;

namespace MQKJ.BSMP.Common
{
    public class DefaultSettingProvider : SettingProvider
    {
        public const string DefaultPageSize = "DefaultPageSize";

        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
                   {
                       new SettingDefinition(DefaultPageSize, "10")
                   };
        }
    }
}
