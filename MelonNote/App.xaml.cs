using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.IO;
using System.Windows;

namespace MelonNote
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App() {
            if (File.Exists(Settings.path))
            {
                Settings.LoadUSettings();
                if (Settings.USettings.data.Count != 0)
                    foreach (var dt in Settings.USettings.data)
                        new MainWindow(dt).Show();
                else StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
            }
            else StartupUri = new Uri("MainWindow.xaml", UriKind.Relative); }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Settings.SaveSettings();
        }
    }
}
