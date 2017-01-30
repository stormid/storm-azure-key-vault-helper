namespace Storm.AzureKeyVaultHelper.Config
{
    public static class KeyVaultHelperSettingsResolver
    {
#if NET452
        public static IKeyVaultHelperSettings Current { get; private set; } // = new FromConfigKeyVaultHelperSettings();
#else
        public static IKeyVaultHelperSettings Current { get; private set; }
#endif

        public static void Set(IKeyVaultHelperSettings settings)
        {
            Current = settings;
        }
    }
}