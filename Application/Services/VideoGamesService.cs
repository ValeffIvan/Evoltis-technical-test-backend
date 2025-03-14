using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class VideoGamesService : IVideoGamesService
    {
        private readonly IVideoGamesRepository _videoGamesRepository;
        private readonly IMapper _mapper;

        public VideoGamesService(IVideoGamesRepository videoGamesRepository, IMapper mapper)
        {
            _videoGamesRepository = videoGamesRepository;
            _mapper = mapper;
        }

        public async Task<VideoGame> CreateVideoGameAsync(VideoGameCreateDto videoGamesCreateDto)
        {   
            var existingVideoGame = await _videoGamesRepository.GetVideoGameByNameAsync(videoGamesCreateDto.Name);
            if (existingVideoGame != null)
            {
                throw new Exception("El videojuego ya existe");
            }

            var newVideoGame = _mapper.Map<VideoGame>(videoGamesCreateDto);

            await _videoGamesRepository.CreateVideoGameAsync(newVideoGame);


            return newVideoGame;
        }


        public async Task<int> DeleteVideoGameAsync(int id)
        {
            return await _videoGamesRepository.DeleteVideoGameAsync(id);
        }


        public async Task<IEnumerable<VideoGameDto>> GetVideoGamesAsync(int pageNumber, int pageSize, VideoGameFilterDto filters)
        {
            var videoGames = await _videoGamesRepository.GetVideoGamesAsync(pageNumber, pageSize, filters);

            var videoGameDtos = videoGames.Select(vg => new VideoGameDto
            {
                Id = vg.Id,
                Name = vg.Name,
                Genre = vg.Genre,
                Note = vg.Note
            });

            return videoGameDtos;
        }

        public async Task<VideoGame> UpdateVideoGameAsync(int id, VideoGameCreateDto videoGameUpdateDto)
        {
            // Buscar el videojuego en el repositorio
            var videoGame = await _videoGamesRepository.GetVideoGameByIdAsync(id);
            if (videoGame == null)
            {
                throw new Exception("El videojuego no encontrado");
            }

            // Mapear las propiedades del DTO al objeto existente usando AutoMapper
            _mapper.Map(videoGameUpdateDto, videoGame);

            // Utilizar la función del repositorio para actualizar el objeto
            return await _videoGamesRepository.UpdateVideoGameAsync(videoGame);
        }


    }
}
