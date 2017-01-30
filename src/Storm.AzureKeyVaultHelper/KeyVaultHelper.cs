using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Storm.AzureKeyVaultHelper.Config;

namespace Storm.AzureKeyVaultHelper
{
    public class KeyVaultHelper : IKeyVaultHelper
    {
        private const string SecretsPath = "secrets";
        private const string KeysPath = "keys";

        private readonly IKeyVaultHelperSettings _settings;
        public KeyVaultHelper(IKeyVaultHelperSettings settings)
        {
            _settings = settings;
        }

        public KeyVaultHelper() : this(KeyVaultHelperSettingsResolver.Current) { }

        private Uri GetSecretUriIdentifier(string name)
        {
            var secretsPath = (_settings as IKeyVaultHelperExtendedSettings)?.SecretsUriPath ?? SecretsPath;
            var secretUri = new Uri($"{_settings.Uri}{secretsPath}/{name}");
            return secretUri;
        }

        private Uri GetKeyUriIdentifier(string name)
        {
            var keysPath = (_settings as IKeyVaultHelperExtendedSettings)?.KeysUriPath ?? KeysPath;
            var keyUri = new Uri($"{_settings.Uri}{keysPath}/{name}");
            return keyUri;
        }

        public async Task PopulateAsync<T>(T settings, CancellationToken cancellationToken = default(CancellationToken))
        {
            var props = typeof(T)
                .GetTypeInfo()
                .GetProperties()
                .Where(p => p.CanWrite)
                .Where(p => p.CustomAttributes.OfType<KeyVaultSecretAttribute>().Any())
                .Select(x => new
                {
                    Property = x,
                    Attribute = x.GetCustomAttribute<KeyVaultSecretAttribute>()
                })
                .ToList();

            foreach (var prop in props)
            {
                try
                {
                    var secretBundle = await GetSecretBundleAsync(prop.Attribute.Name, cancellationToken);
                    prop.Property.SetValue(settings, secretBundle.Value);
                }
                catch (Exception ex)
                {
                    // Trace.TraceError("Failed to set {0} to value from key vault secret {1} : {2}", prop.Property.Name, prop.Attribute.Name, ex.Message);
                    prop.Property.SetValue(settings, prop.Attribute.DefaultValue);
                }

            }
        }

        public async Task<SecretBundle> GetSecretBundleAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            var kv = new KeyVaultClient(GetTokenAsync);
            var secret = await kv.GetSecretAsync(GetSecretUriIdentifier(name).ToString(), cancellationToken);
            return secret;
        }

        public async Task<KeyBundle> GetKeyBundleAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            var kv = new KeyVaultClient(GetTokenAsync);
            var key = await kv.GetKeyAsync(GetKeyUriIdentifier(name).ToString(), cancellationToken);
            return key;
        }

        private async Task<string> GetTokenAsync(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            var clientCred = new ClientCredential(_settings.ClientId, _settings.ClientSecret);
            var result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");

            return result.AccessToken;
        }
    }
}