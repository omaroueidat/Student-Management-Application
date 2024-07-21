using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServicesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class RegionService : IRegionService
	{
		private readonly AppDbContext _context;

		public RegionService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<List<Region>> GetAllRegions(Guid? countryId)
		{
			if (countryId is null)
				return await GetAllRegions();
			var regions = await _context.Regions
				.Where(r => r.CountryId == countryId)
				.ToListAsync();

			return regions;
		}

        public async Task<List<Region>> GetAllRegions()
        {
            var regions = await _context.Regions
				.Include(r => r.Country)
                .ToListAsync();

            return regions;
        }
    }
}
