using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TanksGameEngine.GameEngine;

namespace TanksStory.View.Screens
{
    /// <summary>
    /// Логика взаимодействия для GameProcess_Screen.xaml
    /// </summary>
    public partial class GameProcess_Screen : Page
    {
        MediaPlayer player = new MediaPlayer();

        String SelectedMap;

        GameProcess GProcess;

        Thread thread;

        public GameProcess_Screen(String selMap)
        {
            InitializeComponent();

            this.SelectedMap = selMap;
        }

        private void GameProcess_Page_Loaded(object sender, RoutedEventArgs e)
        {
            GProcess = new GameProcess(this.Field, SelectedMap + ".tmx");

            GProcess.Camera.ViewChanged += Field.Update;

            thread = new Thread(GProcess.StartProcess);

            thread.Priority = ThreadPriority.AboveNormal;

            player.Open(new Uri("Sounds/AirCorn.mp3", UriKind.Relative));

            player.Play(); 

            thread.Start();

            GameObject gOjb = GProcess.gameMap.GetObject("bug");

            if (gOjb != null)
                gOjb.Crashed += Error_Occured;

            

           
            
        }

        private void Media_Ended(object sender, EventArgs e)
        {
            player.Open(new Uri("Sounds/AirCorn.mp3", UriKind.Relative));
            player.Play();
        }

        private void Error_Occured(GameObject gObj)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Error_Screen screen = new Error_Screen();

                ((Frame)App.Current.MainWindow.Content).Navigate(screen);
            });
           
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();

            if (MessageBox.Show("Close Application?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                player.Play();               
            }
            else
            {
                thread.Abort();

                Application.Current.Shutdown();
            }
        }     
    }
}

