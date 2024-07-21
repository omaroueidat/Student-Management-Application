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
    public class AddressService : IAddressService
    {

        private readonly AppDbContext _context;

        public AddressService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddNewAddresses(List<AddressRequest> addresses, Guid StudentId)
        {
            var addressesToInsert = addresses.Select(a => a.ToAddress(StudentId)).ToList();
            await _context.AddRangeAsync(addressesToInsert); 

            return addressesToInsert.Count;
        }


        public Task<List<Address>> GetAllAddresses(Guid studentId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAddress(Guid studentId, Address address)
        {
            var addressToUpdate = await _context.Addresses
                 .FirstOrDefaultAsync(a => a.AddressId == address.AddressId);

            // Address is not found or there is a new address
            if (addressToUpdate is null)
            {

                if (address.StudentId != studentId)
                {
                    return false;
                }
            }

            addressToUpdate.AddressValue = address.AddressValue;
            addressToUpdate.CodeValueId = address.CodeValueId;
            addressToUpdate.RegionId = address.RegionId;
            addressToUpdate.isPrimary = address.isPrimary;

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteAddress(Guid? AddressId)
        {
            var address = await _context.Addresses
                .FirstOrDefaultAsync(a => a.AddressId == AddressId);

            if (address is null)
            {
                return false;
            }

            _context.Addresses.Remove(address);

            await _context.SaveChangesAsync();

            return true;
        }


    }
}
