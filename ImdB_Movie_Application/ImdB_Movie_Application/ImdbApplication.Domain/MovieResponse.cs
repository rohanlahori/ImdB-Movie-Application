using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImdbApplication.Domain
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Int32 YearOfRelease { get; set; }
        public string Plot { get; set; }
        public List<Actor> Actors { get; set; }
        public Producer Producer { get; set; }
    }
}
