using System;
using System.Runtime.InteropServices;

namespace Editor.App.Interop
{
    internal static class DwmApi
    {
        // Win10 dark title bar
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE_OLD = 19;
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        // Win11 extras
        private const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;   // esquinas
        private const int DWMWA_SYSTEMBACKDROP_TYPE     = 38;   // Mica/Acrylic

        // Corner prefs
        private const int DWMWCP_DEFAULT    = 0;
        private const int DWMWCP_DONOTROUND = 1;
        private const int DWMWCP_ROUND      = 2;
        private const int DWMWCP_ROUNDSMALL = 3;

        // Backdrop types
        private const int DWMSBT_AUTO   = 0;
        private const int DWMSBT_NONE   = 1;
        private const int DWMSBT_MAIN   = 2; // Mica
        private const int DWMSBT_TRANSIENT = 3; // Acrylic (semi)
        private const int DWMSBT_TABBED = 4; // Mica (tabbed)

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(
            IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        internal static void SetImmersiveDarkMode(IntPtr hWnd, bool enabled)
        {
            if (hWnd == IntPtr.Zero) return;
            int useDark = enabled ? 1 : 0;
            int r = DwmSetWindowAttribute(hWnd, DWMWA_USE_IMMERSIVE_DARK_MODE, ref useDark, sizeof(int));
            if (r != 0)
                DwmSetWindowAttribute(hWnd, DWMWA_USE_IMMERSIVE_DARK_MODE_OLD, ref useDark, sizeof(int));
        }

        internal static void SetSystemBackdropMica(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) return;
            int mica = DWMSBT_MAIN; // Mica (como Fotos)
            DwmSetWindowAttribute(hWnd, DWMWA_SYSTEMBACKDROP_TYPE, ref mica, sizeof(int));
        }

        internal static void SetRoundedCorners(IntPtr hWnd, bool small = false)
        {
            if (hWnd == IntPtr.Zero) return;
            int pref = small ? DWMWCP_ROUNDSMALL : DWMWCP_ROUND;
            DwmSetWindowAttribute(hWnd, DWMWA_WINDOW_CORNER_PREFERENCE, ref pref, sizeof(int));
        }
    }
}
