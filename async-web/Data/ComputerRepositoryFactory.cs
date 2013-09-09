using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace async_web.Data
{
    public class ComputerRepositoryFactory
    {
        private static string GetDbProvider()
        {
            return ConfigurationManager.AppSettings[ProviderSettingName].ToUpper();
        }

        private const string ProviderSettingName = "DbProvider";
        private const string ProviderEF = "EF";
        private const string ProviderAdo = "ADO";

        public static IComputerAsyncRepository GetAsyncRepository()
        {
            string currentProvider = GetDbProvider();
            switch (currentProvider)
            {
                case ProviderEF:
                    return new ComputerEFAsyncRepository();
                case ProviderAdo:
                    return new ComputerAdoAsyncRepository();
                default:
                    ThrowConfigurtionException(currentProvider);
                    return null;
            }
        }

        private static void ThrowConfigurtionException(string currentProvider)
        {
            throw new InvalidOperationException(String.Format(
                "Wrong '{0}' appSetting configuration value: '{1}'. Allowed values: '{2}', '{3}'",
                ProviderSettingName, currentProvider, ProviderEF, ProviderAdo));
        }

        public static IComputerSyncRepository GetSyncRepository()
        {
            string currentProvider = GetDbProvider();
            switch (currentProvider)
            {
                case ProviderEF:
                    return new ComputerEFSyncRepository();
                case ProviderAdo:
                    return new ComputerAdoSyncRepository();
                default:
                    ThrowConfigurtionException(currentProvider);
                    return null;
            }
        }
    }
}