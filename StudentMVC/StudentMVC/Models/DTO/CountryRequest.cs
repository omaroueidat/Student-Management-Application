using StudentMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMVC.Models.DTO
{
    public class CountryRequest
    {
        public string? CountryName { get; set; }

        public Country ToCountry()
        {
            return new Country() {  CountryName = CountryName };
        }
    }
}
