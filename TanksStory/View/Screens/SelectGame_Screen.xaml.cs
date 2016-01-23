using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ControlsLibrary;
using System.IO;
using System.Collections.ObjectModel;

namespace TanksStory.View.Screens
{
    /// <summary>
    /// Логика взаимодействия для SelectGame_Screen.xaml
    /// </summary>
    public partial class SelectGame_Screen : Page
    {
        public SelectGame_Screen()
        {
            InitializeComponent();

            CycleCollection<Uri> collection = new CycleCollection<Uri>();

            var maps = Directory.EnumerateFiles(Directory.GetCurrentDirectory() + @"\Maps", "*.*", SearchOption.TopDirectoryOnly)
                .Where(s => s.EndsWith(".png") || s.EndsWith(".jpg"));

            foreach(string map in maps)
                collection.Add(new Uri(map, UriKind.Relative));
           
            CarouselMenu.ItemsSources = collection;
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            String mapName = System.IO.Path.GetFileName(CarouselMenu.CurrentItem.OriginalString).Split('.')[0];

            GameProcess_Screen screen = new GameProcess_Screen(mapName);

            ((Frame)App.Current.MainWindow.Content).Navigate(screen);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
