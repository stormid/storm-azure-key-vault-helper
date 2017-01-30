using System.Threading.Tasks;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure.KeyVault.WebKey;
using Storm.AzureKeyVaultHelper.Config;

namespace Storm.AzureKeyVaultHelper.Extensions
{
    public static class KeyVaultExtensions
    {
        public static async Task<SecretBundle> GetSecretBundleFromKeyVaultAsync(this string secretName, IKeyVaultHelperSettings settings = null)
        {
            var manager = new KeyVaultHelper(settings ?? KeyVaultHelperSettingsResolver.Current);
            return await manager.GetSecretBundleAsync(secretName);
        }

        public static async Task<JsonWebKey> GetKeyFromKeyVaultAsync(this string keyName, IKeyVaultHelperSettings settings = null)
        {
            var manager = new KeyVaultHelper(settings ?? KeyVaultHelperSettingsResolver.Current);
            var result = await manager.GetKeyBundleAsync(keyName);
            return result.Key;
        }

        public static async Task<string> GetSecretFromKeyVaultAsync(this string secretName, IKeyVaultHelperSettings settings = null)
        {
            var manager = new KeyVaultHelper(settings ?? KeyVaultHelperSettingsResolver.Current);
            var result = await manager.GetSecretBundleAsync(secretName);
            return result.Value;
        }

        public static async Task<KeyBundle> GetKeyBundleFromKeyVaultAsync(this string keyName, IKeyVaultHelperSettings settings = null)
        {
            var manager = new KeyVaultHelper(settings ?? KeyVaultHelperSettingsResolver.Current);
            return await manager.GetKeyBundleAsync(keyName);
        }
    }
}