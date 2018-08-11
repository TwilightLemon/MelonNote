using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MelonNote
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Settings.Data mData;
        public MainWindow(Settings.Data dt)
        {
            InitializeComponent();
            mData = dt;
            TData.Text = dt.conText;
            TData.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(dt.code);
            Width = dt.Width;
            Height = dt.Height;
            Left = dt.left;
            Top = dt.top;
            List<Color> c = new List<Color>();
            c.Add(Color.FromRgb(255, 185, 1));
            c.Add(Color.FromRgb(17, 137, 5));
            c.Add(Color.FromRgb(217, 1, 169));
            c.Add(Color.FromRgb(93, 36, 155));
            c.Add(Color.FromRgb(1, 121, 215));
            c.Add(Color.FromRgb(119, 119, 119));
            Resources["ThemeColor"] = c[mData.theme];
            c.Clear();
        }
        public MainWindow()
        {
            InitializeComponent();
            mData = new Settings.Data();
            mData.conText = "Welcome to MelonNote!";
            Settings.USettings.data.Add(mData);
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
            if (e.LeftButton == MouseButtonState.Released)
            {
                if (mData.top != Top && mData.left != Left)
                    i = 1;
                mData.top = this.Top;
                mData.left = this.Left;
                mData.Height = this.ActualHeight;
                mData.Width = this.ActualWidth;
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            mData.conText = TData.Text;
        }
        int i = 1;
        private void AddBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Settings.USettings.data.Count == 1)
                i = 1;
            Settings.Data tData = new Settings.Data()
            {
                Width = Width,
                Height = Height,
                left = Left + 50 * i,
                top = Top +50* i,
                code = mData.code,
                theme=mData.theme
            };
            new MainWindow(tData).Show();
            Settings.USettings.data.Add(tData);
            i++;
        }

        private void DeleteBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Settings.USettings.data.Remove(mData);
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Settings.SaveSettings();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Page.Clip = new RectangleGeometry() { RadiusX = 5, RadiusY = 5, Rect = new Rect() { Width = Page.ActualWidth, Height = Page.ActualHeight } };
        }

        private void ThemeColor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            List<Color> c = new List<Color>();
            c.Add(Color.FromRgb(255,185,1));
            c.Add(Color.FromRgb(17,137,5));
            c.Add(Color.FromRgb(217,1,169));
            c.Add(Color.FromRgb(93,36,155));
            c.Add(Color.FromRgb(1,121,215));
            c.Add(Color.FromRgb(119,119,119));
            mData.theme++;
            if (mData.theme == c.Count)
                mData.theme = 0;
            Resources["ThemeColor"] = c[mData.theme];
            c.Clear();
        }
    }
}
