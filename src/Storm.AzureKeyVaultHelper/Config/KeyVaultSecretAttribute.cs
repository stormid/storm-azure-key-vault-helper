using System;

namespace Storm.AzureKeyVaultHelper.Config
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class KeyVaultSecretAttribute : Attribute
    {
        public string Name { get; }
        public object DefaultValue { get; }

        public KeyVaultSecretAttribute(string name, object defaultValue = null)
        {
            Name = name;
            DefaultValue = defaultValue;
        }
    }
}