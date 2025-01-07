using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WordTrainer.Models;
using WordTrainer.Services;
using WordTrainer.Commands;
using WordTrainer.Views;
using System.Diagnostics;

namespace WordTrainer.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly WordService _wordService;
        private readonly StatisticsService _statisticsService;
        private Word _currentWord;
        private List<string> _translationOptions;
        private string _selectedTranslation;
        private Statistics _statistics;
        private string _previousWord;
        private string _correctAnswer;

        public MainViewModel(WordService wordService, StatisticsService statisticsService)
        {
            _wordService = wordService;
            _statisticsService = statisticsService;
            Categories = new ObservableCollection<string>(_wordService.GetCategories().Select(c => c.Name));
            SelectedCategory = Categories.FirstOrDefault();
            LoadCurrentWord();
        }

        public ObservableCollection<string> Categories { get; set; }

        private string _selectedCategory;
        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
                LoadCurrentWord(); // Загрузить новое слово при изменении категории
            }
        }

        public Word CurrentWord
        {
            get { return _currentWord; }
            set
            {
                _currentWord = value;
                OnPropertyChanged(nameof(CurrentWord));
            }
        }

        public List<string> TranslationOptions
        {
            get { return _translationOptions; }
            set
            {
                _translationOptions = value;
                OnPropertyChanged(nameof(TranslationOptions));
            }
        }

        public string SelectedTranslation
        {
            get { return _selectedTranslation; }
            set
            {
                _selectedTranslation = value;
                OnPropertyChanged(nameof(SelectedTranslation));
            }
        }

        public Statistics Statistics
        {
            get { return _statistics; }
            set
            {
                _statistics = value;
                OnPropertyChanged(nameof(Statistics));
            }
        }

        public string CorrectAnswer
        {
            get { return _correctAnswer; }
            set
            {
                _correctAnswer = value;
                OnPropertyChanged(nameof(CorrectAnswer));
            }
        }

        public ICommand CheckAnswerCommand => new RelayCommand(CheckAnswer);

        private void CheckAnswer()
        {
            if (SelectedTranslation == null) return;

            bool isCorrect = SelectedTranslation == CurrentWord.Translation;
            _statisticsService.UpdateStatistics(isCorrect);
            Statistics = _statisticsService.GetStatistics();

            if (!isCorrect)
            {
                CorrectAnswer = $"Correct Answer: {CurrentWord.Translation}";
            }
            else
            {
                CorrectAnswer = null;
            }

            LoadCurrentWord();
        }

        private void LoadCurrentWord()
        {
            var category = _wordService.GetCategories().FirstOrDefault(c => c.Name == SelectedCategory);
            if (category != null && category.Words.Count > 1)
            {
                var availableWords = category.Words.Where(w => w.Original != _previousWord).ToList();
                var allWords = category.Words.ToList();
                if (allWords.Count > 2)
                {
                    CurrentWord = availableWords.OrderBy(w => Guid.NewGuid()).First();
                    _previousWord = CurrentWord.Original;

                    var otherWords = availableWords.Where(w => w.Original != CurrentWord.Original).OrderBy(w => Guid.NewGuid()).Take(3).ToList();
                    TranslationOptions = new List<string> { CurrentWord.Translation };
                    TranslationOptions.AddRange(otherWords.Select(w => w.Translation));
                    TranslationOptions = TranslationOptions.OrderBy(w => Guid.NewGuid()).ToList();
                }
                else
                {
                    CurrentWord = null;
                    TranslationOptions = new List<string>();
                }
            }
            else
            {
                CurrentWord = null;
                TranslationOptions = new List<string>();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}