using Entities.Models;
using ServicesContract.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServicesContract
{
    public interface IAddressService
    {
        Task<int> AddNewAddresses(List<AddressRequest> addresses, Guid StudentId);
        Task<List<Address>> GetAllAddresses(Guid studentId);
        Task<bool> UpdateAddress(Guid studentId, Address address);

        Task<bool> DeleteAddress(Guid? AddressId);
    }
}
