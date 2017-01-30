namespace Storm.AzureKeyVaultHelper.Config
{
    public interface IKeyVaultHelperExtendedSettings : IKeyVaultHelperSettings
    {
        /// <summary>
        /// Path to secrets base identitier, e.g. secrets
        /// </summary>
        string SecretsUriPath { get; }

        /// <summary>
        /// Path to keys base identitier
        /// </summary>
        string KeysUriPath { get; }
    }
}