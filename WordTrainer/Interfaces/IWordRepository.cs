using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordTrainer.Models;

namespace WordTrainer.Interfaces
{
    public interface IWordRepository
    {
        List<Category> LoadCategories();
        void SaveCategories(List<Category> categories);
    }
}
