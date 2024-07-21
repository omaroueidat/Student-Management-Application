using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace ServicesContract
{
    public interface IRegionService
    {

        Task<List<Region>> GetAllRegions(Guid? countryId);
        Task<List<Region>> GetAllRegions();
    }
}
