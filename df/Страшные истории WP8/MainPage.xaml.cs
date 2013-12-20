using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using System.IO.IsolatedStorage;
using System.Xml.Linq;
using System.IO;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Marketplace;

namespace Страшные_истории_wp8
{
    public partial class MainPage : PhoneApplicationPage
    {
        IsolatedStorageSettings appsettings = IsolatedStorageSettings.ApplicationSettings;

        public class NewsArticle
        {
            public string Title { get; set; }//заголовок
            public string Text { get; set; }//текст статьи
            public int Id { get; set; }//идентификационный номер
            public string Author { get; set; }//имя автора
            public string Img { get; set; }//ссылку на картинку
            public string Date { get; set; }//дату создания
        }

        public class VideoArticle
        {
            public string title { get; set; }
            public string Video { get; set; }
            public int IDD { get; set; }
            public string IAuthor { get; set; }
            public string IImg { get; set; }
            public string IDate { get; set; }
            public string tt { get; set; }
        }

        private const string TASK_NAME = "Agent";
        IsolatedStorageFile ttt = IsolatedStorageFile.GetUserStoreForApplication();
        int newsCount = 0;
        const string StateKey = "MessageState";
        public MainPage()
        {
            InitializeComponent();
        }

        #region навигация
        private void TextBlock_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {

               NavigationService.Navigate(new Uri("/Video.xaml", UriKind.Relative));
        }

        private void TextBlock_Tap_2(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Истории.xaml", UriKind.Relative));
        }

        private void StackPanel_Tap_3(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Author.xaml", UriKind.Relative));
        }

        private void StackPanel_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Setting.xaml", UriKind.Relative));
        }

        private void StackPanel_Tap_2(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }

        private void StackPanel_Tap_4(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var mysender = sender as StackPanel;
            NavigationService.Navigate(new Uri("/Article.xaml?id=" + mysender.Tag.ToString(), UriKind.Relative));
        }

        private void StackPanel_Tap_5(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MarketplaceReviewTask a = new MarketplaceReviewTask();
            a.Show();
        }

        private void StackPanel_Tap_8(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var mysender = sender as Grid;
            WebClient m = new WebClient();
            m.DownloadStringCompleted += m_DownloadStringCompleted;
            m.DownloadStringAsync(new Uri(String.Format(mysender.Tag.ToString(), new Random().Next())));
        }
        #endregion

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            #region сообщение
            if (appsettings.Contains("massege"))
            {
                appsettings["massege"] = "no";
            }
            else
            {
                appsettings.Add("massege", "no");
                MessageBoxResult mssg = MessageBox.Show("Вы хотите получать уведомления о новых страшных историях?", "Уведомлять о новых историях?", MessageBoxButton.OKCancel);
                if (mssg == MessageBoxResult.OK)
                {
                    if (appsettings.Contains("toast"))
                    {
                        appsettings["toast"] = "yes";
                    }
                    else
                    {
                        appsettings.Add("toast", "yes");
                    }
                }
                else
                {
                    if (appsettings.Contains("toast"))
                    {
                        appsettings["toast"] = "no";
                    }
                    else
                    {
                        appsettings.Add("toast", "no");
                    }
                }
            }
            #endregion

            #region плитка
            ShellTile firsttile = ShellTile.ActiveTiles.First();
            if (firsttile != null)
            {
                var tile = new FlipTileData();
                tile.BackTitle = "";
                tile.BackContent = "";
                firsttile.Update(tile);
            }
            #endregion

            try
            {
                using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (isoStore.FileExists("StoriesTitle.xml"))
                    {
                        using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("StoriesTitle.xml", FileMode.Open, isoStore))
                        {
                            newsCount = 0;
                            XDocument xElem = XDocument.Load(isoStream);
                            List<NewsArticle> info = new List<NewsArticle>();
                            foreach (XElement el in xElem.Root.Elements())
                            {
                                newsCount++;
                                info.Add(new NewsArticle()
                                {
                                    Id = Convert.ToInt32(el.Attribute("id").Value),
                                    Title = el.Attribute("title").Value,
                                    Img = "http://pakrom.ucoz.ru/Pictures/Stories/" + el.Attribute("id").Value + ".jpg",
                                    Author = "АВТОР: " + el.Attribute("author").Value,
                                });

                                if (newsCount == 4) { break; }

                            }
                            Story.ItemsSource = info;
                            int stories = Convert.ToInt32(xElem.Root.Element("Article").Attribute("id").Value);
                            IsolatedStorageSettings.ApplicationSettings["count"] = stories;
                            IsolatedStorageSettings.ApplicationSettings.Save();
                        }
                    }
                }
            }
            catch
            { }

            try
            {
                IsolatedStorageFile isoStoree = IsolatedStorageFile.GetUserStoreForApplication();
                {
                    if (isoStoree.FileExists("Video.xml"))
                    {
                        IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("Video.xml", FileMode.Open, isoStoree);
                        int videoCount = 0;
                        XDocument xElem = XDocument.Load(isoStream);
                        List<VideoArticle> videoo = new List<VideoArticle>();
                        foreach (XElement el in xElem.Root.Elements())
                        {
                            videoCount++;
                            videoo.Add(new VideoArticle()
                            {
                                IDD = Convert.ToInt32(el.Attribute("id").Value),//конвертируем в Integer (значение аттрибута id)
                                Video = el.Attribute("Video").Value,
                                IImg = el.Attribute("img").Value,
                                title = el.Attribute("title").Value,
                            });
                            if (videoCount == 4) { break; }
                        }
                        Video.ItemsSource = videoo;
                    }
                }
            }
            catch
            {  }
            WebClient webstory = new WebClient();
            WebClient webvideo = new WebClient();
            webstory.DownloadStringCompleted += WebstoryCompleted;
            webvideo.DownloadStringCompleted += WebvideoCompleted;
            webstory.DownloadStringAsync(new Uri(String.Format("http://pakrom.ucoz.ru/Stories/StoriesTitle.xml?n={0}", new Random().Next())));
            webvideo.DownloadStringAsync(new Uri(String.Format("http://pakrom.ucoz.ru/Pictures/Video/Video.xml?n={0}", new Random().Next())));
        }

        private void WebvideoCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    if (e.Result.StartsWith("<?xml"))
                    {
                        StreamWriter file = new StreamWriter(new IsolatedStorageFileStream("Video.xml", FileMode.Create, isoStore));
                        XDocument xElem = XDocument.Parse(e.Result, LoadOptions.None);
                        xElem.Save(file);
                        if (xElem != null)
                        {
                            int videoCount = 0;
                            List<VideoArticle> videooo = new List<VideoArticle>();
                            foreach (XElement el in xElem.Root.Elements())
                            {
                                videoCount++;
                                videooo.Add(new VideoArticle()
                                {
                                    IDD = Convert.ToInt32(el.Attribute("id").Value),
                                    Video = el.Attribute("Video").Value,
                                    IImg = el.Attribute("img").Value,
                                    title = el.Attribute("title").Value,
                                });
                                if (videoCount == 4) { break; }
                            }
                            Video.ItemsSource = videooo;

                        }
                        file.Close();
                    }
                }
                catch 
                {

                }
            }
        }

        private void WebstoryCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            //if (Story.Items.Count == 0)
            //{
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    if (e.Result.StartsWith("<?xml"))
                    {
                        using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("StoriesTitle.xml", FileMode.Create, isoStore))
                        {
                            XDocument xElem = XDocument.Parse(e.Result, LoadOptions.None);
                            xElem.Save(isoStream);

                            if (xElem != null)
                            {
                                newsCount = 0;
                                List<NewsArticle> info = new List<NewsArticle>();
                               
                                foreach (XElement el in xElem.Root.Elements())
                                {
                                    newsCount++;
                                    info.Add(new NewsArticle()
                                    {
                                        Id = Convert.ToInt32(el.Attribute("id").Value),
                                        Title = el.Attribute("title").Value,
                                        Img = "http://pakrom.ucoz.ru/Pictures/Stories/" + el.Attribute("id").Value + ".jpg",
                                        Author = "АВТОР: " + el.Attribute("author").Value,
                                    });
                                    if (newsCount == 4) { break; }
                                }
                                Story.ItemsSource = info;
                                int stories = Convert.ToInt32(xElem.Root.Element("Article").Attribute("id").Value);
                                IsolatedStorageSettings.ApplicationSettings["count"] = stories;
                                IsolatedStorageSettings.ApplicationSettings.Save();
                                if (appsettings.Contains("toast"))
                                {
                                    if ((string)appsettings["toast"] == "yes")
                                    {
                                        StartAgent();
                                    }
                                    else
                                    {
                                        if (ScheduledActionService.Find(TASK_NAME) != null)
                                        {
                                            StopAgentIfStarted();
                                        }
                                    }
                                }
                                else
                                {
                                    {
                                        appsettings.Add("toast", "yes");
                                        if (ScheduledActionService.Find(TASK_NAME) == null)
                                        {
                                            StartAgent();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                catch //(Exception ex)
                {
                }
            }

        }

        private void StartAgent()
        {
            try
            {
                StopAgentIfStarted();
                PeriodicTask task = new PeriodicTask(TASK_NAME);
                task.Description = "Страшные истории";
                ScheduledActionService.Add(task);
#if DEBUG
                ScheduledActionService.LaunchForTest(TASK_NAME, new TimeSpan(0, 0, 1));
#endif
            }
            catch { }
        }

        private void StopAgentIfStarted()
        {
            if (ScheduledActionService.Find(TASK_NAME) != null)
            {
                ScheduledActionService.Remove(TASK_NAME);
            }
        }

        private void m_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                MediaPlayerLauncher mediaPlayerLauncher = new MediaPlayerLauncher();
                mediaPlayerLauncher.Media = new Uri(e.Result.Split(new string[] { "<source src=\"" }, StringSplitOptions.None)[1].Replace('"', '?'), UriKind.Absolute);
                //IImg = e.Result.Split(new string[] { "<img src=\"" }, StringSplitOptions.None)[1].Replace('"', '?');
                mediaPlayerLauncher.Controls = MediaPlaybackControls.All;
                mediaPlayerLauncher.Location = MediaLocationType.Data;
                mediaPlayerLauncher.Orientation = MediaPlayerOrientation.Landscape;
                mediaPlayerLauncher.Show();
            }
            catch { MessageBox.Show("Сейчас это кино не доступно для просмотра.", "Извините!", MessageBoxButton.OK); }
        }
    }
}