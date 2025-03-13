using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAuth
    {
        Task<UsuarioResponseDto> LoginAsync(string email, string password);

        Task<UsuarioResponseDto> Register(string name, string email, string password);
    }
}
