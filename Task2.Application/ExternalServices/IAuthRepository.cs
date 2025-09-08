using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Dtos.AuthDtos;
using Task2.Application.Result;

namespace Task2.Application.ExternalServices
{
    public interface IAuthRepository
    {
        public Task<bool> UserExist(string Email);
        public Task<CustomResult<string>> Register(RegistrationRequestDto registration);
        public Task<CustomResult<string>> Login(LoginRequestDto loginRequest);
    }
}
