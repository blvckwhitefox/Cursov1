using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordTrainer.Interfaces;
using WordTrainer.Models;
using System.IO;
using System.Text.Json; 

namespace WordTrainer.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly string _filePath;

        public StatisticsRepository(string filePath)
        {
            _filePath = filePath;
        }

        public Statistics LoadStatistics()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<Statistics>(json);
            }
            return new Statistics();
        }

        public void SaveStatistics(Statistics statistics)
        {
            var json = JsonSerializer.Serialize(statistics);
            File.WriteAllText(_filePath, json);
        }
    }
}
