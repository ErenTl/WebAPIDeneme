using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIDeneme.Models
{
    public class movie
    {
        public int id { get; set; }
        public string movieTitle { get; set; }

        public DateTime releaseDate { get; set; }

        public Decimal imdbRank { get; set; }

    }
}
