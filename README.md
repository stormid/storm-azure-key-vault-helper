# Storm.AzureKeyVaultHelper

Provides a helper over the Azure Key Vault client library, further simplifying the retrieval of keys and secrets.

## Installation

Via nuget:

`
  install-package Storm.AzureKeyVaultHelper
`

## Examples

### Basic extension method usage
```c#
  var secret = await "secret-name".GetSecretFromKeyVaultAsync();
  var secretBundle = await "secret-name".GetSecretBundleFromKeyVaultAsync();
  
  var jsonWebKey = await "key-name".GetKeyFromKeyVaultAsync();
  var keyBundle = await "secret-name".GetKeyBundleFromKeyVaultAsync();
```

### Helper usage
```c#
  var vaultManager = new KeyVaultHelper(KeyVaultHelperSettingsResolver.Current);
  var secretBundle = vaultManager.GetSecretBundleAsync("secret-name");
  var keyBundle = await vaultManager.GetKeyBundleAsync("key-name");
```

## Configuration

The ```KeyVaultHelper``` accepts a settings interface that can be implemented as required, with a default implementation provided that will read required configuration settings from the ```.config``` file.

The ```KeyVaultHelperSettingsResolver``` static class will return the default configuration implementation ```FromConfigKeyVaultHelperSettings()``` from its ```.Current``` property with a ```.Set(IKeyVaultHelperSettings settings)``` method available to change the settings implementation returned from the resolver.

### Required settings

```xml
  <!-- ClientId and ClientSecret refer to the web application registration with Azure Active Directory -->
  <add key="KeyVault:ClientId" value="" />
  <add key="KeyVault:ClientSecret" value="" />
  <add key="KeyVault:Uri" value="https://<yourvault>.vault.azure.net" />
```

# Configuration of Azure Key Vault
How to setup Azure Key Vault is beyond the scope of this readme and the library itself, please refer to the Azure Key Vault documentation [here](https://docs.microsoft.com/en-us/azure/key-vault/).
