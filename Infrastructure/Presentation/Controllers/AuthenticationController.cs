using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DataTransferObjects.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManger _serviceManger): ApiBaseController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var User = await _serviceManger.AuthenticationServices.LoginAsync(loginDto);
            return Ok(User);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var User = await _serviceManger.AuthenticationServices.RegisterAsync(registerDto);
            return Ok(User);
        }
        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string Email)
        {
            var Result = await _serviceManger.AuthenticationServices.CheckEmailAsync(Email);
            return Ok(Result);
        }
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            //var email = User.FindFirstValue(ClaimTypes.Email);
            var AppUser =await _serviceManger.AuthenticationServices.GetCurrentUserAsync(GetEmailFromToken()!);
            return Ok(AppUser);
        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDTO>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Address = await _serviceManger.AuthenticationServices.GetCurrentUserAddressAsync(email);
            return Ok(Address);
        }
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDTO>> UpdateCurrentUserAddress(AddressDTO addressDTO)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var UpdateAddress = await _serviceManger.AuthenticationServices.UpdateCurrentUserAddressAsync(email,addressDTO);
            return Ok(UpdateAddress);
        }
    }
}
