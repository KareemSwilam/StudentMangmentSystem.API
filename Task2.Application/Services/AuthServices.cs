using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Dtos.AuthDtos;
using Task2.Application.ExternalServices;
using Task2.Application.Result;
using Task2.Application.Services.IServices;

namespace Task2.Application.Services
{
    public class AuthServices: IAuthServices
    {
        private readonly IAuthRepository _authrepository;
        public AuthServices(IAuthRepository authrepository)
        {
            _authrepository = authrepository;
        }
        public async Task<CustomResult<string>> Register(RegistrationRequestDto registerdto)
        {
            var userexist = await _authrepository.UserExist(registerdto.Email);
            if (userexist)
                return CustomResult<string>.Failure(CustomError.ValidationError("This Email Already Exist"));
            var result = await _authrepository.Register(registerdto);
            return result;
        }
        public async Task<CustomResult<string>> Login(LoginRequestDto requestDto)
        {
            return await _authrepository.Login(requestDto);
        }
    }
}
