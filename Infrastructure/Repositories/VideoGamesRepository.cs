using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class VideoGamesRepository : IVideoGamesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public VideoGamesRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VideoGame> CreateVideoGameAsync(VideoGame videoGames)
        {
            _context.VideoGames.Add(videoGames);
            await _context.SaveChangesAsync();
            return videoGames;
        }

        public async Task<int> DeleteVideoGameAsync(int id)
        {
            return await _context.VideoGames
                .Where(vg => vg.Id == id)
                .ExecuteDeleteAsync();
        }

        public Task<VideoGame> GetVideoGameByNameAsync(string name)
        {
            return _context.VideoGames.FirstOrDefaultAsync(vg => vg.Name == name);
        }

        public async Task<IEnumerable<VideoGame>> GetVideoGamesAsync(int pageNumber, int pageSize, VideoGameFilterDto filters)
        {
            var query = _context.VideoGames.AsQueryable();

            if (filters.Id.HasValue)
            {
                query = query.Where(vg => vg.Id == filters.Id.Value);
            }

            if (!string.IsNullOrEmpty(filters.Name))
            {
                query = query.Where(vg => vg.Name.Contains(filters.Name));
            }

            if (!string.IsNullOrEmpty(filters.Genre))
            {
                query = query.Where(vg => vg.Genre.Contains(filters.Genre));
            }

            if (filters.Note.HasValue)
            {
                query = query.Where(vg => vg.Note == filters.Note.Value);
            }


            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<VideoGame> UpdateVideoGameAsync(VideoGame videoGame)
        {
            _context.VideoGames.Update(videoGame);
            await _context.SaveChangesAsync();
            return videoGame;
        }


        public Task<VideoGame> GetVideoGameByIdAsync(int id)
        {
            return _context.VideoGames.FirstOrDefaultAsync(vg => vg.Id == id);
        }
    }
}
