using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault.Models;

namespace Storm.AzureKeyVaultHelper
{
    public interface IKeyVaultHelper
    {
        Task<SecretBundle> GetSecretBundleAsync(string name, CancellationToken cancellationToken = default(CancellationToken));
        Task<KeyBundle> GetKeyBundleAsync(string name, CancellationToken cancellationToken = default(CancellationToken));
        Task PopulateAsync<T>(T settings, CancellationToken cancellationToken = default(CancellationToken));
    }
}