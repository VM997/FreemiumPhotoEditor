using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Editor.Core.History;
using Editor.Core.Models;
using System.Windows.Interop;
using Editor.App.Interop;

namespace Editor.App
{
    public partial class MainWindow : Window
    {
        private readonly HistoryManager _history;
        private string? _currentFilePath;

        public MainWindow()
        {
            InitializeComponent();

            SourceInitialized += (_, __) =>
            {
                var hwnd = new WindowInteropHelper(this).Handle;
                DwmApi.SetImmersiveDarkMode(hwnd, true); // título oscuro
                DwmApi.SetSystemBackdropMica(hwnd);      // Mica en la barra (como Fotos)
                DwmApi.SetRoundedCorners(hwnd, small:false); // esquinas redondeadas
            };
            var empty = new byte[] { 0, 0, 0, 0 };
            _history = new HistoryManager(new ImageState(empty, 1, 1));
            UpdateButtons();
            UpdateDocumentTitle();
            UpdateMaximizeIcon();

            StateChanged += (_, __) => UpdateMaximizeIcon();
        }


        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog { Filter = "Images|*.png;*.jpg;*.jpeg" };
            if (dlg.ShowDialog() != true) return;

            var bytes = File.ReadAllBytes(dlg.FileName);

            var bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.CacheOption = BitmapCacheOption.OnLoad;
            bmp.UriSource = new Uri(dlg.FileName, UriKind.Absolute);
            bmp.EndInit();
            bmp.Freeze();

            Preview.Source = bmp;
            _history.Push(new ImageState(bytes, bmp.PixelWidth, bmp.PixelHeight));
            _currentFilePath = dlg.FileName;
            UpdateButtons();
            UpdateDocumentTitle();
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            if (_history.CanUndo) { _history.Undo(); UpdateButtons(); }
        }

        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            if (_history.CanRedo) { _history.Redo(); UpdateButtons(); }
        }

        private void UpdateButtons()
        {
            UndoBtn.IsEnabled = _history.CanUndo;
            RedoBtn.IsEnabled = _history.CanRedo;
        }

        private void UpdateDocumentTitle()
        {
            var displayName = string.IsNullOrEmpty(_currentFilePath)
                ? "Freemium Photo Editor"
                : Path.GetFileName(_currentFilePath);

            DocumentTitle.Text = displayName;
            Title = string.IsNullOrEmpty(_currentFilePath)
                ? "Freemium Photo Editor"
                : $"{displayName} - Freemium Photo Editor";
        }

        private void OnToggleThemeClick(object sender, RoutedEventArgs e)
        {
            ThemeManager.Toggle();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeRestore_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateMaximizeIcon()
        {
            if (MaximizeRestoreBtn is null) return;

            var isMaximized = WindowState == WindowState.Maximized;
            MaximizeRestoreBtn.Content = isMaximized ? "\uE923" : "\uE922";
            MaximizeRestoreBtn.ToolTip = isMaximized ? "Restore" : "Maximize";
        }

        private void TopBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                // Doble clic para maximizar/restaurar
                WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            }
            else
            {
                DragMove(); // arrastrar la ventana
            }
        }
    }
}
