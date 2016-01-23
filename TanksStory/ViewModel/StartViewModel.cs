using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TanksGameEngine.GameEngine;
using TanksStory.Commands;


namespace TanksStory.ViewModel
{
    public class StartViewModel : BaseViewModel
    {
        public ICommand ClickCommand { get; set; }

        public StartViewModel()
        {
            ClickCommand = new DelegateCommand(arg => Started_Clicked()); 
        }

        private void Started_Clicked()
        {
            ((Frame)App.Current.MainWindow.Content).Navigate(new Uri("View\\Screens\\SelectGame_Screen.xaml", UriKind.Relative));

        }

    }
}
