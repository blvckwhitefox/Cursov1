using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WordTrainer.Interfaces;
using WordTrainer.Models;
using System.IO;

namespace WordTrainer.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly string _filePath;

        public WordRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<Category> LoadCategories()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Category>>(json);
            }
            return new List<Category>();
        }

        public void SaveCategories(List<Category> categories)
        {
            var json = JsonSerializer.Serialize(categories);
            File.WriteAllText(_filePath, json);
        }
    }
}
