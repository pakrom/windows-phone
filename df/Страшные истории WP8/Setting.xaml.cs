using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using Windows.Phone.System.UserProfile;

namespace Страшные_истории_wp8
{
    public partial class Setting : PhoneApplicationPage
    {
        string fileName = "A.jpg";
        IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();
        IsolatedStorageSettings appsettings = IsolatedStorageSettings.ApplicationSettings;
        int h = 20;
        public Setting()
        {
            this.Loaded += Setting_Loaded;
            InitializeComponent();
            ContentPanel.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
        }

        void Setting_Loaded(object sender, RoutedEventArgs e)
        {
            ShellTile tile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("secondary"));
            if (tile != null)
            {
                Lt.IsChecked = true;
            }
            if (tile == null)
            {
                Lt.IsChecked = false;
            }
            if (appsettings.Contains("toast"))
            {
                if ((string)appsettings["toast"] == "yes")
                {
                    Push.IsChecked = true;
                    Push.Content = "Включена";
                }
                else
                {
                    Push.IsChecked = false;
                    Push.Content = "Отключена";
                }
            }
            else
            {
                {
                    appsettings.Add("toast", "yes");
                }
                Push.IsChecked = true;
                Push.Content = "Включена";
            }
        }

        private void Push_Checked(object sender, RoutedEventArgs e)
        {
            if (appsettings.Contains("toast"))
            {
                appsettings["toast"] = "yes";
            }
            else
            {
                appsettings.Add("toast", "yes");
            }
            Push.Content = "Включена";
            //Storyboard1.Begin();
        }

        private void Push_Unchecked(object sender, RoutedEventArgs e)
        {
            if (appsettings.Contains("toast"))
            {
                appsettings["toast"] = "no";
            }
            else
            {
                appsettings.Add("toast", "no");
            }
            Push.Content = "Отключена";

            //Storyboard2.Begin();

        }

        private void Lt_Checked(object sender, RoutedEventArgs e)
        {
            Lt.Content = "Включена";
            UpdateCycleTile("Страшные истории", 0, new Uri("/mainpage.xaml?secondary=1", UriKind.Relative), new Uri("/Assets/Tiles/FlipCycleTileSmall.png", UriKind.Relative),
    new List<Uri>()
                {
                    new Uri("/images/anim2/1.jpg", UriKind.Relative),
                    new Uri("/images/anim2/2.jpg", UriKind.Relative),
                    new Uri("/images/anim2/3.jpg", UriKind.Relative),
                    new Uri("/images/anim2/4.jpg", UriKind.Relative),
                    new Uri("/images/anim2/5.jpg", UriKind.Relative),
                    new Uri("/images/anim2/" + new Random().Next(9,14).ToString() + ".jpg", UriKind.Relative),
                    new Uri("/images/anim2/" + new Random().Next(14,17).ToString() + ".jpg", UriKind.Relative),
                    new Uri("/images/anim2/" + new Random().Next(17,20).ToString() + ".jpg", UriKind.Relative),
                });
        }

        public void UpdateCycleTile(string title, int count, Uri tileId, Uri smallBackgroundImage, List<Uri> CycleImages)
        {
            var mytile = new CycleTileData
            {
                Title = title,
                Count = count,
                SmallBackgroundImage = smallBackgroundImage,
                CycleImages = CycleImages
            };
            try
            {
                ShellTile.Create(tileId, mytile, true);
            }
            catch { }
        }

        private void Lt_Unchecked(object sender, RoutedEventArgs e)
        {
            Lt.Content = "Отключена";
            ShellTile thetile = ShellTile.ActiveTiles.FirstOrDefault(tile => tile.NavigationUri.ToString().Contains("/mainpage.xaml?secondary=1"));
            if (thetile != null)
            {
                thetile.Delete();
            }
        }
    }
}