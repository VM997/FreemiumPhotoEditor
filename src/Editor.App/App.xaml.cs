using System.Windows;

namespace Editor.App
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ThemeManager.InitFromSettings(); // Carga Light/Dark al arrancar
        }
    }
}
