using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAuthService
    {
        Task<UserResponseDto> LoginAsync(UserLoginDto userLoginDto);

        Task<UserResponseDto> RegisterAsync(UserRegisterDto userRegisterDto);
    }
}
