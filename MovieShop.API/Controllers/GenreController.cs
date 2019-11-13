using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using MovieShop.Entities;
using MovieShop.Services;
using MoviesShop.Services;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase //base dont have view
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }


        [HttpGet]
        [Route("")]


        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genreService.GetAllGenres();
            if (genres.Any())
            {
                return Ok(genres);
            }

            return NotFound();
        }
        [HttpGet]
        [Route("{id}/movies")]
        public async Task<IActionResult> GetMovieByGenre(int id)
        {
            var movie = await _genreService.GetMoviebyGenre(id);
            return Ok (movie);        
        }


    }
}