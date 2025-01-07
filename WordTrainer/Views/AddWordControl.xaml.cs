using System.Windows.Controls;
using WordTrainer.Services;
using WordTrainer.ViewModels;

namespace WordTrainer.Views
{
    public partial class AddWordControl : UserControl
    {
        public AddWordControl(WordService wordService)
        {
            InitializeComponent();
            DataContext = new AddWordViewModel(wordService);
        }
    }
}
