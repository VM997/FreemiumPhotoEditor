using System;
using System.Windows;

namespace Editor.App
{
    public enum Theme { Light, Dark }

    public static class ThemeManager
    {
        public static Theme Current { get; private set; } = Theme.Dark;

        public static void Apply(Theme theme)
        {
            var dict = new ResourceDictionary
            {
                Source = new Uri($"Styles/Theme.{theme}.xaml", UriKind.Relative)
            };

            var appRes = Application.Current.Resources.MergedDictionaries;
            appRes.Clear();
            appRes.Add(dict);

            Current = theme;

            // Persistir preferencia
            Properties.Settings.Default.Theme = theme.ToString();
            Properties.Settings.Default.Save();
        }

        public static void InitFromSettings()
        {
            if (Enum.TryParse(Properties.Settings.Default.Theme, out Theme saved))
                Apply(saved);
            else
                Apply(Theme.Dark);
        }

        public static void Toggle() =>
            Apply(Current == Theme.Dark ? Theme.Light : Theme.Dark);
    }
}
