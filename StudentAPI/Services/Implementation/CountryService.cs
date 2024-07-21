using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServicesContract;
using ServicesContract.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class CountryService : ICountryService
	{
		private readonly AppDbContext _context;

		public CountryService(AppDbContext context)
		{
			_context = context;
		}

        public async Task<bool> AddNewCountry(CountryRequest country)
        {
            await _context.Countries.AddAsync(country.ToCountry());

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCountry(Guid countryId)
        {
            var country = await _context.Countries
                .SingleOrDefaultAsync(c => c.CountryId == countryId);

            if (country is null)
            {
                return false;
            }

            _context.Countries.Remove(country);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Country>> FilterCountries(string? searchBy, string? searchString)
        {
            List<Country> countries = await GetAllCountries();

            if (searchBy is null || searchString is null)
            {
                return countries;
            }

            switch (searchBy)
            {
                case nameof(Country.CountryId):
                    countries = countries.Where(c => c.CountryId.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(Country.CountryName):
                    countries = countries.Where(c => c.CountryName.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
            }

            return countries;
        }

        public async Task<List<Country>> GetAllCountries()
		{
			var countries = await _context.Countries
					.ToListAsync();

			return countries;
		}

        public async Task<Country?> GetCountry(Guid CountryId)
        {
			var country = await _context.Countries
				.SingleOrDefaultAsync(c => c.CountryId == CountryId);

			return country;

        }

        public async Task<bool> UpdateCountry(Country country)
        {
            var countryToUpdate = await GetCountry(country.CountryId);

            if (countryToUpdate is null)
            {
                return false;
            }

            countryToUpdate.CountryName = country.CountryName;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
