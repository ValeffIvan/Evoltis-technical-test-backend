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
    public class VideoGameMapper : Profile
    {
        public VideoGameMapper()
        {
            CreateMap<VideoGame, VideoGameCreateDto>();
            CreateMap<VideoGameCreateDto, VideoGame>();
        }
    }
}
