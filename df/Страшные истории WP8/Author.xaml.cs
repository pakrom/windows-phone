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
    public partial class Author : PhoneApplicationPage
    {
        public Author()
        {
            InitializeComponent();
        }

        private void ListBoxItem_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://vk.com/pakrom");
            wbt.Show();
        }

        private void ListBoxItem_Tap_2(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://www.facebook.com/profile.php?id=100005073373788");
            wbt.Show();
        }

        private void ListBoxItem_Tap_3(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://spaces.ru/mysite/?name=pakrom");
            wbt.Show();
        }

        private void ListBoxItem_Tap_4(object sender, System.Windows.Input.GestureEventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();
            emailComposeTask.Subject = "Письмо автору";
            emailComposeTask.To = "pakrom@live.ru";
            emailComposeTask.Show();
        }
    }
}