using System;

namespace Storm.AzureKeyVaultHelper.Config
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FromKeyVaultAttribute : Attribute
    {
        
    }
}