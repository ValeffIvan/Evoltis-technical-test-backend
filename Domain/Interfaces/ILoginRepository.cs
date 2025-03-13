using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ILoginRepository
    {
        Task<User> LoginAsync(string email, string password);

        Task<User> RegisterAsync(User user);

        Task<User> GetUserByEmailAsync(string email);
    }
}
