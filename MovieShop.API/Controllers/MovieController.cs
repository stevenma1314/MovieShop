using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Services;
using MoviesShop.Services;

namespace MovieShop.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("Topgrossing")]
        public async Task<IActionResult> GetTopGrossing()

        {
            var movies = await _movieService.GetHighestMovie();
            return Ok (movies);
        }
        [HttpGet]
        [Route("Topgrossing2")]
        public async Task<IActionResult> GetTopGrossing2()

        {
            var movies = await _movieService.GetHighestMovie();
            return Ok(movies);
        }
        
    }
}