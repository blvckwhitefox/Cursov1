using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordTrainer.Interfaces;
using WordTrainer.Models;
using WordTrainer.Repositories;

namespace WordTrainer.Services
{
    public class WordService
    {
        private readonly WordRepository _wordRepository;

        public WordService(WordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public List<Category> GetCategories()
        {
            return _wordRepository.LoadCategories();
        }

        public void AddWord(Word word)
        {
            var categories = GetCategories();
            var category = categories.FirstOrDefault(c => c.Name == word.Category);
            if (category != null)
            {
                category.Words.Add(word);
            }
            else
            {
                categories.Add(new Category { Name = word.Category, Words = new List<Word> { word } });
            }
            _wordRepository.SaveCategories(categories);
        }

        public void AddCategory(string categoryName)
        {
            var categories = GetCategories();
            if (!categories.Any(c => c.Name == categoryName))
            {
                categories.Add(new Category { Name = categoryName, Words = new List<Word>() });
                _wordRepository.SaveCategories(categories);
            }
        }
    }
}