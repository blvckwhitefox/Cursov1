using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using WordTrainer.Commands;
using WordTrainer.Models;
using WordTrainer.Services;

namespace WordTrainer.ViewModels
{
    public class AddWordViewModel : INotifyPropertyChanged
    {
        private readonly WordService _wordService;
        private string _original;
        private string _translation;
        private string _selectedCategory;
        private string _newCategory;

        public event EventHandler CategoryAdded;

        public AddWordViewModel(WordService wordService)
        {
            _wordService = wordService;
            Categories = new ObservableCollection<string>(_wordService.GetCategories().Select(c => c.Name));
        }

        public ObservableCollection<string> Categories { get; set; }

        public string Original
        {
            get { return _original; }
            set
            {
                _original = value;
                OnPropertyChanged(nameof(Original));
            }
        }

        public string Translation
        {
            get { return _translation; }
            set
            {
                _translation = value;
                OnPropertyChanged(nameof(Translation));
            }
        }

        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        public string NewCategory
        {
            get { return _newCategory; }
            set
            {
                _newCategory = value;
                OnPropertyChanged(nameof(NewCategory));
            }
        }

        public ICommand AddWordCommand => new RelayCommand(AddWord);
        public ICommand AddCategoryCommand => new RelayCommand(AddCategory);

        private void AddWord()
        {
            var word = new Word { Original = Original, Translation = Translation, Category = SelectedCategory };
            _wordService.AddWord(word);
        }

        private void AddCategory()
        {
            if (!string.IsNullOrEmpty(NewCategory))
            {
                _wordService.AddCategory(NewCategory);
                Categories.Add(NewCategory);
                SelectedCategory = NewCategory; // Установить новую категорию как выбранную
                NewCategory = string.Empty;
                CategoryAdded?.Invoke(this, EventArgs.Empty); // Уведомить главное окно о добавлении новой категории
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}