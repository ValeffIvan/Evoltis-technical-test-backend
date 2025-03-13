using Domain.DTOs;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class LoginRepository : IAuth
    {
        public Task<UsuarioResponseDto> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioResponseDto> Register(string name, string email, string password)
        {
            throw new NotImplementedException();
        }

    }
}
