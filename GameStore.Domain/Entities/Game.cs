using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    public class Game
    {
        public int GameID { get; set; }
        public String Name { get; set; }
        public String Descriptions { get; set; }
        public String Category { get; set; }
        public decimal Price { get; set; }
    }
}
