using System.Windows.Controls;
using WordTrainer.Services;
using WordTrainer.ViewModels;

namespace WordTrainer.Views
{
    public partial class TestControl : UserControl
    {
        public TestControl(WordService wordService, StatisticsService statisticsService)
        {
            InitializeComponent();
            DataContext = new MainViewModel(wordService, statisticsService);
        }
    }
}
