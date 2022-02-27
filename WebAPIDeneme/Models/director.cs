using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIDeneme.Models
{
    public class director
    {
        public int id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public DateTime dateOfBirth { get; set; }

        public DateTime dateOfDeath { get; set; }

        public sbyte SEX { get; set; }

        public string? spouse { get; set; }


    }
}
