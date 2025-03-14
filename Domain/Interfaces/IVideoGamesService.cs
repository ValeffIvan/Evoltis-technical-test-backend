using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IVideoGamesService
    {
        Task<IEnumerable<VideoGameDto>> GetVideoGamesAsync(int pageNumber, int pageSize, VideoGameFilterDto filters);
        Task<VideoGame> CreateVideoGameAsync(VideoGameCreateDto videoGamesCreateDto);
        Task<VideoGame> UpdateVideoGameAsync(int id, VideoGameCreateDto VideoGameUpdateDto);
        Task<int> DeleteVideoGameAsync(int id);
    }


}
