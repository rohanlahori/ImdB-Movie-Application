using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImdbApplication.Domain
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Int32 YearOfRelease { get; set; }
        public string Plot { get; set; }
        public List<int> Actors { get; set; }
        public Int32 Producer { get; set; }
    }
}
