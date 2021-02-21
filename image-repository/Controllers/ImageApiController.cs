using image_repository.Interfaces;
using image_repository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace image_repository.Controllers {
  [Route("api/image")]
  [ApiController]
  public class ImageApiController : ControllerBase {

    private readonly IImageService _imageService;

    public ImageApiController(IImageService imageService) {
      _imageService = imageService;
    }

    // GET: api/<ImageApiController>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImageModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get() {
      var obj = _imageService.Get(string.Empty);

      if(obj is null) {
        return NotFound();
      }
      return Ok(obj);
    }

    // GET api/<ImageApiController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImageModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id) {
      var obj = await _imageService.GetById(id);

      if(obj == null) {
        return NotFound();
      }
      return Ok(obj);
    }

    // POST api/<ImageApiController>
    [HttpPost]
    public void Post([FromBody] ImageModel model) {

    }

    // PUT api/<ImageApiController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value) {
    }

    // DELETE api/<ImageApiController>/5
    [HttpDelete("{id}")]
    public void Delete(int id) {
    }
  }
}
