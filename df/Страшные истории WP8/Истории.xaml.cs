using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.Xml.Linq;
using System.IO;

namespace Страшные_истории_wp8
{
    public partial class Истории : PhoneApplicationPage
    {
        public class NewsArticle
        {
            public string Title { get; set; }
            public int Id { get; set; }
            public string Author { get; set; }
            public string Img { get; set; }
        }

        public Истории()
        {
            InitializeComponent();
            this.Loaded += Истории_Loaded;
        }

        void Истории_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (isoStore.FileExists("StoriesTitle.xml"))
                    {
                        using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("StoriesTitle.xml", FileMode.Open, isoStore))
                        {
                            int newsCount = 0;
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
                                    //Date = el.Attribute("date").Value,
                                    //Text = el.Attribute("text").Value
                                });

                                
                                //dop.Visibility = System.Windows.Visibility.Visible;

                            }
                            Story.ItemsSource = info;

                        }
                    }
                }
            }
            catch
            { }
        }

        //private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        //{
            //try
            //{
            //    using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            //    {
            //        if (isoStore.FileExists("StoriesTitle.xml"))
            //        {
            //            using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("StoriesTitle.xml", FileMode.Open, isoStore))
            //            {
            //                int newsCount = 0;
            //                XDocument xElem = XDocument.Load(isoStream);
            //                List<NewsArticle> info = new List<NewsArticle>();
            //                foreach (XElement el in xElem.Root.Elements())
            //                {
            //                    newsCount++;
            //                    info.Add(new NewsArticle()
            //                    {
            //                        Id = Convert.ToInt32(el.Attribute("id").Value),
            //                        Title = el.Attribute("title").Value,
            //                        Img = "http://pakrom.ucoz.ru/Pictures/Stories/" + el.Attribute("id").Value + ".jpg",
            //                        Author = "АВТОР: " + el.Attribute("author").Value,
            //                        //Date = el.Attribute("date").Value,
            //                        //Text = el.Attribute("text").Value
            //                    });

            //                    if (newsCount == 4) { break; }
            //                    //dop.Visibility = System.Windows.Visibility.Visible;

            //                }
            //                Story.ItemsSource = info;
                           
            //            }
            //        }
            //    }
            //}
            //catch
            //{ }
            //try
            //{
            //    using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            //    {
            //        if (isoStore.FileExists("StoriesTitle.xml"))
            //        {
            //            using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("StoriesTitle.xml", FileMode.Open, isoStore))
            //            {
            //                XDocument xElem = XDocument.Load(isoStream);
            //                int count = 0;
            //                List<NewsArticle> info = new List<NewsArticle>();
            //                foreach (XElement el in xElem.Root.Elements())
            //                {
            //                    count++;
            //                    info.Add(new NewsArticle()
            //                    {
            //                        Id = Convert.ToInt32(el.Attribute("id").Value),
            //                        Img = "http://pakrom.ucoz.ru/Pictures/Stories/" + el.Attribute("id").Value + ".jpg",
            //                        Author = "АВТОР: " + el.Attribute("author").Value,
            //                        Title = el.Attribute("title").Value,
            //                    });
            //                    if (count == 25)
            //                    {
            //                        break;
            //                    }
            //                }
            //                Story.ItemsSource = info;
            //            }
            //        }
            //    }
            //}
            //catch { }
        //}

        private void StackPanel_Tap_4(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var mysender = sender as StackPanel;
            NavigationService.Navigate(new Uri("/Article.xaml?id=" + mysender.Tag.ToString(), UriKind.Relative));
        }

        private void dop_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //dop.Visibility = System.Windows.Visibility.Collapsed;
            try
            {
                using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (isoStore.FileExists("StoriesTitle.xml"))
                    {
                        using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("StoriesTitle.xml", FileMode.Open, isoStore))
                        {
                            XDocument xElem = XDocument.Load(isoStream);
                            List<NewsArticle> info = new List<NewsArticle>();
                            foreach (XElement el in xElem.Root.Elements())
                            {
                                info.Add(new NewsArticle()
                                {
                                    Id = Convert.ToInt32(el.Attribute("id").Value),
                                    Img = "http://pakrom.ucoz.ru/Pictures/Stories/" + el.Attribute("id").Value + ".jpg",
                                    Author = "АВТОР: " + el.Attribute("author").Value,
                                    Title = el.Attribute("title").Value,
                                });
                            }
                            Story.ItemsSource = info;
                        }
                    }
                }
            }
            catch { }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var mysender = sender as MenuItem;
            NavigationService.Navigate(new Uri("/API.xaml?id=" + mysender.Tag.ToString() + "&line=вконтакте", UriKind.Relative));
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            var mysender = sender as MenuItem;
            NavigationService.Navigate(new Uri("/API.xaml?id=" + mysender.Tag.ToString() + "&line=facebook", UriKind.Relative));
        }
    }
}