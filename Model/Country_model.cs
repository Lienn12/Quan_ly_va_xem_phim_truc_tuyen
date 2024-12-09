using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Model
{
    public class Country_model
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public Country_model() { }
        public Country_model(int countryId, string countryName)
        {
            CountryId = countryId;
            CountryName = countryName;
        }

        public override string ToString()
        {
            return CountryName;
        }
    }
}
