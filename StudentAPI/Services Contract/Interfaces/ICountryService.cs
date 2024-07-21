using Entities.Models;
using ServicesContract.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesContract
{
    public interface ICountryService
    {
        public Task<List<Country>> GetAllCountries();
        public Task<Country?> GetCountry(Guid CountryId);
        public Task<List<Country>> FilterCountries(string? searchBy, string? searchString);
        public Task<bool> AddNewCountry(CountryRequest country);
        public Task<bool> DeleteCountry(Guid countryId);
        public Task<bool> UpdateCountry(Country country);
    }
}
