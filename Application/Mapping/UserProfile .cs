using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<User, UserResponseDto>();
            CreateMap<UserRegisterDto, User>();
        }

    }
}
