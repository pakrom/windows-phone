using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using System.Xml.Linq;
using System.IO.IsolatedStorage;
using System.Net;
using System;
using Microsoft.Phone.Shell;
using System.IO;
using System.Linq;

namespace Agent
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        IsolatedStorageSettings appsettings = IsolatedStorageSettings.ApplicationSettings;
        public string title { get; set; }
        /// <remarks>
        /// Конструктор ScheduledAgent, инициализирует обработчик UnhandledException
        /// </remarks>
        static ScheduledAgent()
        {
            // Подпишитесь на обработчик управляемых исключений
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// Код для выполнения на необработанных исключениях
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // Произошло необработанное исключение; перейти в отладчик
                Debugger.Break();
            }
        }

        /// <summary>
        /// Агент, запускающий назначенное задание
        /// </summary>
        /// <param name="task">
        /// Вызванная задача
        /// </param>
        /// <remarks>
        /// Этот метод вызывается при запуске периодических или ресурсоемких задач
        /// </remarks>
        protected override void OnInvoke(ScheduledTask task)
        {
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += загрузка;
            wc.DownloadStringAsync(new Uri(String.Format("http://pakrom.ucoz.ru/Stories/StoriesTitle.xml?n={0}", new Random().Next())));
        }

        int element;
        private void загрузка(object sender, DownloadStringCompletedEventArgs e)
        {
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())//запрашиваем место для хранения файлов приложением
            {
                try//пытаемся выполнить код в кавычках, если будет ошибка - продолжится выполняться код в кавычках catch{}
                {
                    if (e.Result.StartsWith("<?xml"))//е - переменная с результатами загрузки, которая дается клиентом по окончании загрузки. проверяем, начинается ли файл с <?xml
                    {

                        using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("StoriesTitle.xml", FileMode.Create, isoStore))//создаем файл в хранилище
                        {
                            XDocument xElem = XDocument.Parse(e.Result, LoadOptions.None);
                            xElem.Save(isoStream);
                            if (xElem != null)//если файл не пустой
                            element = Convert.ToInt32(xElem.Root.Element("Article").Attribute("id").Value);
                            title = xElem.Root.Element("Article").Attribute("title").Value;
                            if ((int)IsolatedStorageSettings.ApplicationSettings["count"] < element)
                            {
                                IsolatedStorageSettings.ApplicationSettings["count"] = element;
                                IsolatedStorageSettings.ApplicationSettings.Save();
                                ShellToast toast = new ShellToast();
                                toast.Title = "Истории:";
                                toast.Content = title;
                                toast.NavigationUri = new Uri("/Article.xaml?id=" + element.ToString(), UriKind.Relative);
                                toast.Show();

                                ShellTile firsttile = ShellTile.ActiveTiles.First();
                                if (firsttile != null)
                                {
                                    var tile = new FlipTileData();
                                    tile.BackTitle = "Страшные истории";
                                    tile.BackContent = "Новая история:" + Environment.NewLine + title;
                                    firsttile.Update(tile);
                                }
                            }
                        }
                    }
                }
                catch { }
                NotifyComplete();
            }
        }
    }
}
