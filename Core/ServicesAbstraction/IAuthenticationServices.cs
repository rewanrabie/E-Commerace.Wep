using Shared.DataTransferObjects.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IAuthenticationServices
    {
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task<UserDto> RegisterAsync(RegisterDto registerDto);

        Task<bool> CheckEmailAsync(string Email);
        Task<AddressDTO> GetCurrentUserAddressAsync(string Email);

        Task<AddressDTO> UpdateCurrentUserAddressAsync(string Email,AddressDTO addressDTO);
        Task<UserDto> GetCurrentUserAsync(string Email);
    }
}
