using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Net;
using System.IO;

namespace MoonSec
{
    /// <summary>
    /// Interaction logic for LoadWindow.xaml
    /// </summary>
    public partial class LoadWindow : Window
    {
        public LoadWindow()
        {
            InitializeComponent();

            DoubleAnimation da = new DoubleAnimation(0, 360, new Duration(TimeSpan.FromSeconds(1)), FillBehavior.Stop);
            da.RepeatBehavior = RepeatBehavior.Forever;
            da.EasingFunction = new QuarticEase() { EasingMode = EasingMode.EaseInOut };

            var rot = new RotateTransform();
            loadingAnimation.RenderTransform = rot;
            loadingAnimation.RenderTransformOrigin = new Point(0.5, 0.5);

            rot.BeginAnimation(RotateTransform.AngleProperty, da);
        }

        public void Load()
        {
            this.Hide();
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // checking server accessibility
            try
            {
                var req = WebRequest.CreateHttp("https://api.f3d.at/v1/obfuscate.php");
                await req.GetResponseAsync();
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    Load();
                }
                else {
                    MessageBox.Show("Error occured. Server might be unaccessible.");
                }
            }
            catch (Exception ex)
            {
                StreamWriter sw = new StreamWriter("errors.log", true);
                sw.WriteLine(string.Format("{0} in {1}", ex.Message, ex.StackTrace));
                sw.Close();
            }
        }
    }
}
