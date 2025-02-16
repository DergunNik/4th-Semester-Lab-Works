using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrigadeManager.Domain.Entities
{
    public class Brigade : Entity
    {
        public string Name { get; set; }  
        public string Leader { get; set; }
        public int WorkersNumber { get; set; }
        public List<Work> Works { get; set; } = new();
    }
}
