namespace Storm.AzureKeyVaultHelper.Config
{
#if NET452
    using System;

    public class FromConfigKeyVaultHelperSettings : IKeyVaultHelperSettings
    {
        private const string Prefix = "KeyVault:";
        public string ClientId { get; } = nameof(ClientId).FromAppSettingsWithPrefix(Prefix);
        public string ClientSecret { get; } = nameof(ClientSecret).FromAppSettingsWithPrefix(Prefix);
        public Uri Uri { get; } = new Uri(nameof(Uri).FromAppSettingsWithPrefix(Prefix));
    }
#endif
}