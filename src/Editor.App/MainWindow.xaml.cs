using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Editor.Core.History;
using Editor.Core.Models;

namespace Editor.App
{
    public partial class MainWindow : Window
    {
        private HistoryManager _history;

        public MainWindow()
        {
            InitializeComponent();

            var empty = new byte[] { 0, 0, 0, 0 };
            _history = new HistoryManager(new ImageState(empty, 1, 1));
            UpdateButtons();
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
            UpdateButtons();
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
    }
}
