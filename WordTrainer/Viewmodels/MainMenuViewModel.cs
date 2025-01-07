using System.ComponentModel;
using System.Windows.Input;
using WordTrainer.Commands;
using WordTrainer.Services;
using WordTrainer.Views;

namespace WordTrainer.ViewModels
{
    public class MainMenuViewModel : INotifyPropertyChanged
    {
        private readonly WordService _wordService;
        private readonly StatisticsService _statisticsService;
        private object _currentView;
        private bool _isTestEnabled = true;
        private bool _isAddWordEnabled = true;

        public MainMenuViewModel(WordService wordService, StatisticsService statisticsService)
        {
            _wordService = wordService;
            _statisticsService = statisticsService;
            CurrentView = new TestControl(_wordService, _statisticsService);
            IsTestEnabled = false;
        }

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
                UpdateButtonStates();
            }
        }

        public bool IsTestEnabled
        {
            get { return _isTestEnabled; }
            set
            {
                _isTestEnabled = value;
                OnPropertyChanged(nameof(IsTestEnabled));
            }
        }

        public bool IsAddWordEnabled
        {
            get { return _isAddWordEnabled; }
            set
            {
                _isAddWordEnabled = value;
                OnPropertyChanged(nameof(IsAddWordEnabled));
            }
        }

        public ICommand OpenTestWindowCommand => new RelayCommand(OpenTestWindow);
        public ICommand OpenAddWordWindowCommand => new RelayCommand(OpenAddWordWindow);

        private void OpenTestWindow()
        {
            CurrentView = new TestControl(_wordService, _statisticsService);
        }

        private void OpenAddWordWindow()
        {
            CurrentView = new AddWordControl(_wordService);
        }

        private void UpdateButtonStates()
        {
            IsTestEnabled = !(CurrentView is TestControl);
            IsAddWordEnabled = !(CurrentView is AddWordControl);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
