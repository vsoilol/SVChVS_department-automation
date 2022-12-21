using System.Collections.Generic;
using System.Linq;
using CommonLibrary.StringDecryptor;
using Microsoft.Extensions.Configuration;

namespace DepartmentAutomation.Shared.StringDecryptor
{
    public static class HashStringUtil
    {
        private const string EnvironmentSectionName = "Local";
        private const string ConnectionStringsSectionName = "ConnectionStrings";

        public static string GetValueFromHash(string hash)
        {
            return hash.GetDecryptedString();
        }

        public static string GetDecryptedConnectionString(
            this IConfiguration configuration,
            string dbConnectionName)
        {
            var stringFromConfiguration = configuration
                .GetSection(ConnectionStringsSectionName)
                .GetSection(EnvironmentSectionName)
                .GetValue<string>(dbConnectionName);

            return GetValueFromHash(stringFromConfiguration);
        }

        public static List<string> GetDecryptedCorsOrigins(
            this IConfiguration configuration)
        {
            return GetValueFromHash(
                    configuration
                        .GetSection("Settings")
                        .GetSection(EnvironmentSectionName)
                        .GetValue<string>("CorsOrigins"))
                .Split(',')
                .ToList();
        }
    }
}