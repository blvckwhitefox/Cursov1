using System.Windows;
using WordTrainer.Repositories;
using WordTrainer.Services;
using WordTrainer.Views;
using WordTrainer.ViewModels;

namespace WordTrainer
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var wordRepository = new WordRepository("Data/words.json");
            var statisticsRepository = new StatisticsRepository("Data/statistics.json");
            var wordService = new WordService(wordRepository);
            var statisticsService = new StatisticsService(statisticsRepository);

            var mainMenuViewModel = new MainMenuViewModel(wordService, statisticsService);
            var mainMenu = new MainMenu { DataContext = mainMenuViewModel };
            mainMenu.Show();
        }
    }
}
