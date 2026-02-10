using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientKit.UnitTesting.Models
{
    internal class Country
    {
        public string? Name { get; set; }
        public string? OfficialName { get; set; }

        public string? Region { get; set; }
        public long Population { get; set; }

        public Country()
        {
            
        }

        public Country(string commonName, string officialName, string region, long population)
        {
            Name = commonName;
            OfficialName = officialName;
            Region = region;
            Population = population;
        }

        public static readonly Country[] Countries =
        [
            new("Andorra", "Principality of Andorra", "Europe", 88406),
            new("Belarus", "Republic of Belarus", "Europe", 9109280),
            new("Germany", "Federal Republic of Germany", "Europe", 83491249),
            new("France", "French Republic", "Europe", 66351959),
            new("United Kingdom", "United Kingdom of Great Britain and Northern Ireland", "Europe", 69281437),
        ];
    }

}
