using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IVideoGamesRepository
    {
        Task<IEnumerable<VideoGame>> GetVideoGamesAsync(int pageNumber, int pageSize, VideoGameFilterDto filters);
        Task<VideoGame> GetVideoGameByNameAsync(string name);
        Task<VideoGame> CreateVideoGameAsync(VideoGame videoGames);
        Task<VideoGame> UpdateVideoGameAsync(VideoGame videoGames);
        Task<int> DeleteVideoGameAsync(int id);
        Task<VideoGame> GetVideoGameByIdAsync(int id);

    }
}
