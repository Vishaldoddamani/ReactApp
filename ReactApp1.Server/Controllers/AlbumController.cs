using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AlbumApi.Models;
using AlbumApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 
using JsonSerializer = System.Text.Json.JsonSerializer;
using AlbumApi.Entities;
using Microsoft.AspNetCore.Cors;


namespace AlbumApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase

    {

        private readonly ILogger<AlbumController> _logger;
        private IAlbumService _albumService { get; }
        private IMapper _mapper { get; }

        public AlbumController(ILogger<AlbumController> logger, IAlbumService aLbumService, IMapper mapper)
        {
            _logger = logger;
            _albumService = aLbumService;
            _mapper = mapper;
        }

         

        #region "GetAlbumDetails"

        /// <summary>
        /// https://localhost:44369/api/Album/GetAlbumDetails?UserId=1 or swagger documentation.
        ///  Get Album details by UserId.
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <returns>List of albums for a particular UserId.</returns>

        [HttpGet(Name = "GetAlbumDetails")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int userId)
        {
            try
            {
                int? UserId = 1;
                List<Album> listAlbum = null;
                List<Photo> listPhotos = null;

                if (UserId == null)
                {
                    _logger.LogError("User ID is null");

                    return BadRequest();
                }
                else
                {
                    var userAlbums = await _albumService.GetAlbumsAsync(UserId);

                    var mappedAlbum = _mapper.Map<List<AlbumDetailsDTO>>(userAlbums);

                    if (userAlbums == null)
                    {
                        return NotFound();
                    }

                    return Ok(userAlbums);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occurred in Method GetFromNewtonJson");
                throw;
            }

        }
        #endregion




    }
}
