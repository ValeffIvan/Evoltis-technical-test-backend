using Application.Services;
using Domain.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoGamesController : Controller
    {
        private readonly IVideoGamesService _videoGamesService;

        public VideoGamesController(IVideoGamesService videoGamesService)
        {
            _videoGamesService = videoGamesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoGameDto>>> Get(
            [FromQuery] VideoGameFilterDto filters,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var videoGames = await _videoGamesService.GetVideoGamesAsync(pageNumber, pageSize, filters);
            return Ok(videoGames);
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] VideoGameCreateDto videoGameCreateDto)
        {
            if (videoGameCreateDto == null)
            {
                return BadRequest("Invalid video game data.");
            }

            try
            {
                var createdVideoGame = await _videoGamesService.CreateVideoGameAsync(videoGameCreateDto);
                return Ok(createdVideoGame);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletedVideoGame = await _videoGamesService.DeleteVideoGameAsync(id);
                if (deletedVideoGame == null)
                {
                    return NotFound();
                }
                return Ok(deletedVideoGame);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VideoGameCreateDto videoGameUpdateDto)
        {
            if (videoGameUpdateDto == null)
            {
                return BadRequest("Invalid video game data.");
            }

            try
            {
                var updatedVideoGame = await _videoGamesService.UpdateVideoGameAsync(id, videoGameUpdateDto);
                return Ok(updatedVideoGame);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }


}
