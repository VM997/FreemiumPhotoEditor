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
                DwmApi.SetImmersiveDarkMode(hwnd, true);        // título oscuro
                DwmApi.SetSystemBackdropMica(hwnd);             // Mica en la barra (como Fotos)
                DwmApi.SetRoundedCorners(hwnd, small: false);   // esquinas redondeadas
            };

            // Historial inicial con un estado vacío
            var empty = new byte[] { 0, 0, 0, 0 };
            _history = new HistoryManager(new ImageState(empty, 1, 1));

            UpdateUI();
            UpdateDocumentTitle();
            UpdateMaximizeIcon();

            StateChanged += (_, __) => UpdateMaximizeIcon();
        }

        // === Estado derivado ===
        private bool HasImage => Preview?.Source != null;

        // === Abrir imagen ===
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

            // Empujamos el estado base de la imagen al historial
            _history.Push(new ImageState(bytes, bmp.PixelWidth, bmp.PixelHeight));

            _currentFilePath = dlg.FileName;

            UpdateUI();
            UpdateDocumentTitle();
        }

        // === Editar (placeholder) ===
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (!HasImage) return;

            // Aquí iría tu lógica de edición (brillo, rotación, B&N, etc.)
            // Al finalizar, mete el nuevo estado en el historial:
            // var (bytes,width,height) = RenderOrGetCurrentImageState();
            // _history.Push(new ImageState(bytes, width, height));

            UpdateUI();
        }

        // === Undo / Redo ===
        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            if (_history.CanUndo)
            {
                _history.Undo();

                // TODO: si tu HistoryManager expone el estado actual, rehidrátalo:
                // var state = _history.Current;
                // Preview.Source = BytesToBitmapImage(state.Bytes);

                UpdateUI();
            }
        }

        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            if (_history.CanRedo)
            {
                _history.Redo();

                // TODO: rehidratar imagen si procede:
                // var state = _history.Current;
                // Preview.Source = BytesToBitmapImage(state.Bytes);

                UpdateUI();
            }
        }

        // === UI state ===
        private void UpdateUI()
        {
            var hasImage = HasImage;

            // Mostrar/ocultar botones según si hay imagen
            if (EditBtn != null)  EditBtn.Visibility  = hasImage ? Visibility.Visible : Visibility.Collapsed;
            if (UndoBtn != null)  UndoBtn.Visibility  = hasImage ? Visibility.Visible : Visibility.Collapsed;
            if (RedoBtn != null)  RedoBtn.Visibility  = hasImage ? Visibility.Visible : Visibility.Collapsed;

            // Habilitar undo/redo sólo con imagen y si el historial lo permite
            if (UndoBtn != null)  UndoBtn.IsEnabled   = hasImage && _history.CanUndo;
            if (RedoBtn != null)  RedoBtn.IsEnabled   = hasImage && _history.CanRedo;
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

        // Helper opcional si quieres rehidratar desde bytes:
        private static BitmapImage BytesToBitmapImage(byte[] bytes)
        {
            using var ms = new MemoryStream(bytes);
            var bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.CacheOption = BitmapCacheOption.OnLoad;
            bmp.StreamSource = ms;
            bmp.EndInit();
            bmp.Freeze();
            return bmp;
        }
    }
}
