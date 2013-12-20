using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using System.IO;
using System.Xml.Linq;
using System.Windows.Media;
using System.Net;

namespace Страшные_истории_wp8
{
    public partial class Article : PhoneApplicationPage
    {
        IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();
        public Article()
        {
            InitializeComponent();
            история.Begin();
            история.Completed += история_Completed;
        }

        private void история_Completed(object sender, EventArgs e)
        {
            история.Begin();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("Stories" + NavigationContext.QueryString["id"] + ".xml", FileMode.Open, isoStore))
                {
                    XDocument xElem = XDocument.Load(isoStream);
                    if (xElem != null)
                    {
                        title.Text = xElem.Root.Element("Article").Attribute("title").Value;
                        date.Text = xElem.Root.Element("Article").Attribute("date").Value;
                        author.Text = xElem.Root.Element("Article").Attribute("author").Value;
                        image.Source = (ImageSource)new ImageSourceConverter().ConvertFromString("http://pakrom.ucoz.ru/Pictures/Stories/" + NavigationContext.QueryString["id"] + ".jpg");
                        foreach (string s in xElem.Root.Element("Article").Attribute("text").Value.Split(new string[] { "#" }, StringSplitOptions.None))
                        {
                            text.Children.Add(new TextBlock()
                            {
                                Text = s,
                                FontSize = 30,
                                Foreground = new SolidColorBrush((Color)App.Current.Resources["PhoneForegroundColor"]),
                                TextWrapping = System.Windows.TextWrapping.Wrap,
                                Margin = new Thickness(24, 0, 0, 12)
                            });
                        }
                    }
                }
            }
            catch { }
            WebClient bn = new WebClient();
            bn.DownloadStringCompleted += bn_DownloadStringCompleted;
            bn.DownloadStringAsync(new Uri(String.Format("http://pakrom.ucoz.ru/Stories/" + NavigationContext.QueryString["id"] + ".xml?n={0}", new Random().Next())));
        }

        void bn_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Result.StartsWith("<?xml"))
                {
                    IsolatedStorageFileStream isofile = new IsolatedStorageFileStream("Stories" + NavigationContext.QueryString["id"] + ".xml", FileMode.Create, isoStore);
                    XDocument xElem = XDocument.Parse(e.Result, LoadOptions.None);
                    xElem.Save(isofile);
                    if (xElem != null)
                    {
                        title.Text = xElem.Root.Element("Article").Attribute("title").Value;
                        date.Text = xElem.Root.Element("Article").Attribute("date").Value;
                        author.Text = xElem.Root.Element("Article").Attribute("author").Value;
                        image.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(xElem.Root.Element("Article").Attribute("img").Value);
                        foreach (string s in xElem.Root.Element("Article").Attribute("text").Value.Split(new string[] { "#" }, StringSplitOptions.None))
                        {
                            text.Children.Add(new TextBlock()
                            {
                                Text = s,
                                FontSize = 30,
                                Foreground = new SolidColorBrush((Color)App.Current.Resources["PhoneForegroundColor"]),
                                TextWrapping = System.Windows.TextWrapping.Wrap,
                                Margin = new Thickness(24, 0, 0, 12)
                            });
                        }                     
                    }
                }
            }
            catch { }
        }

        private void Button_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            h.ScrollIntoView(h.Items[0]);
        }

        private void image_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }
    }
}