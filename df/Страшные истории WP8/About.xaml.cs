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

namespace Страшные_истории_wp8
{
    public partial class About : PhoneApplicationPage
    {
        public About()
        {
            InitializeComponent();
        }

        private void ListBoxItem_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://vk.com/ofdark");
            wbt.Show();
        }

        private void ListBoxItem_Tap_2(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://of-dark.blogspot.ru/");
            wbt.Show();
        }

        private void ListBoxItem_Tap_3(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://spaces.ru/comm/?com=38901");
            wbt.Show();
        }

        private void Button_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MarketplaceReviewTask a = new MarketplaceReviewTask();
            a.Show();
        }
    }
}