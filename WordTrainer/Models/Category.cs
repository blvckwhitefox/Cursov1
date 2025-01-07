using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordTrainer.Models
{
    public class Category
    {
        public string Name { get; set; }
        public List<Word> Words { get; set; }
    }
}
