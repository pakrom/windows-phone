﻿#pragma checksum "C:\Users\Александр\Documents\Visual Studio 2012\Projects\Страшные истории WP8\Страшные истории wp8\Article.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CE32465C62741420D266424A52C23A94"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.18033
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Страшные_истории_wp8 {
    
    
    public partial class Article : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Media.Animation.Storyboard история;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.ListBox h;
        
        internal System.Windows.Controls.TextBlock date;
        
        internal System.Windows.Controls.TextBlock author;
        
        internal System.Windows.Controls.TextBlock title;
        
        internal System.Windows.Controls.StackPanel images;
        
        internal System.Windows.Controls.Image image;
        
        internal System.Windows.Controls.StackPanel text;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/TS;component/Article.xaml", System.UriKind.Relative));
            this.история = ((System.Windows.Media.Animation.Storyboard)(this.FindName("история")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.h = ((System.Windows.Controls.ListBox)(this.FindName("h")));
            this.date = ((System.Windows.Controls.TextBlock)(this.FindName("date")));
            this.author = ((System.Windows.Controls.TextBlock)(this.FindName("author")));
            this.title = ((System.Windows.Controls.TextBlock)(this.FindName("title")));
            this.images = ((System.Windows.Controls.StackPanel)(this.FindName("images")));
            this.image = ((System.Windows.Controls.Image)(this.FindName("image")));
            this.text = ((System.Windows.Controls.StackPanel)(this.FindName("text")));
        }
    }
}

