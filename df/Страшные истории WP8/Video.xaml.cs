using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.IO.IsolatedStorage;
using System.Xml.Linq;
using System.IO;

namespace Страшные_истории_wp8
{
    public partial class Video : PhoneApplicationPage
    {
        IsolatedStorageFile isofile = IsolatedStorageFile.GetUserStoreForApplication();
        public class VideoArticle
        {
            public string title { get; set; }
            public string Video { get; set; }
            public string IImg { get; set; }
        }
        public Video()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                //IsolatedStorageFile isoStoree = IsolatedStorageFile.GetUserStoreForApplication();
                {
                    if (isofile.FileExists("Video.xml"))
                    {
                        IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("Video.xml", FileMode.Open, isofile);
                        int videoCount = 0;
                        XDocument xxElem = XDocument.Load(isoStream);
                        List<VideoArticle> videoo = new List<VideoArticle>();
                        foreach (XElement el in xxElem.Root.Elements())
                        {
                            videoCount++;
                            videoo.Add(new VideoArticle()
                            {
                                Video = el.Attribute("Video").Value,
                                IImg = el.Attribute("img").Value,
                                title = el.Attribute("title").Value,
                            });
                        }
                        video.ItemsSource = videoo;
                    }
                }
            }
            catch { }
        }

        private void StackPanel_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var mysender = sender as StackPanel;
            WebClient m = new WebClient();
            m.DownloadStringCompleted += m_DownloadStringCompleted;
            m.DownloadStringAsync(new Uri(String.Format(mysender.Tag.ToString(), new Random().Next())));
        }

        private void m_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                MediaPlayerLauncher mediaPlayerLauncher = new MediaPlayerLauncher();
                mediaPlayerLauncher.Media = new Uri(e.Result.Split(new string[] { "<source src=\"" }, StringSplitOptions.None)[1].Replace('"', '?'), UriKind.Absolute);
                mediaPlayerLauncher.Controls = MediaPlaybackControls.All;
                mediaPlayerLauncher.Location = MediaLocationType.Data;
                mediaPlayerLauncher.Orientation = MediaPlayerOrientation.Landscape;
                mediaPlayerLauncher.Show();
            }
            catch { MessageBox.Show("Сейчас это кино не доступно для просмотра.", "Извините!", MessageBoxButton.OK); }

        }
    }
}