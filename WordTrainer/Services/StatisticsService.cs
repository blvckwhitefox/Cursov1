using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordTrainer.Interfaces;
using WordTrainer.Models;

namespace WordTrainer.Services
{
    public class StatisticsService
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsService(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public Statistics GetStatistics()
        {
            return _statisticsRepository.LoadStatistics();
        }

        public void UpdateStatistics(bool isCorrect)
        {
            var statistics = _statisticsRepository.LoadStatistics();
            if (isCorrect)
            {
                statistics.CorrectAnswers++;
            }
            else
            {
                statistics.IncorrectAnswers++;
            }
            _statisticsRepository.SaveStatistics(statistics);
        }
    }
}
