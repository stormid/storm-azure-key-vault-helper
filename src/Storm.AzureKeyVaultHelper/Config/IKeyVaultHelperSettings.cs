using System;

namespace Storm.AzureKeyVaultHelper.Config
{
    public interface IKeyVaultHelperSettings
    {
        /// <summary>
        /// Application client Id
        /// </summary>
        string ClientId { get; }

        /// <summary>
        /// Application client secret
        /// </summary>
        string ClientSecret { get; }

        /// <summary>
        /// Base Uri to vault: https://myvault.vault.azure.net
        /// </summary>
        Uri Uri { get; }
    }
}