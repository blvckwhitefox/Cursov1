using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordTrainer.Models;

namespace WordTrainer.Interfaces
{
    public interface IStatisticsRepository
    {
        Statistics LoadStatistics();
        void SaveStatistics(Statistics statistics);
    }
}
