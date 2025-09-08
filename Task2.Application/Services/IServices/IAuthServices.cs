using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Dtos.AuthDtos;
using Task2.Application.Result;

namespace Task2.Application.Services.IServices
{
    public interface IAuthServices
    {
        public Task<CustomResult<string>> Register(RegistrationRequestDto registerdto);
        public Task<CustomResult<string>> Login(LoginRequestDto requestDto);
    }
}