using InternalAPI.Models;
using InternalAPI.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace InternalAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[EnableCors("AllowSpecificOrigin")]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;
    
    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }
    
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Generate(GenerateImageModel model)
    {
        if (model.prompt.Length == 0)
        {
            return BadRequest("Provide a prompt.");
        }

        return Ok(await _imageService.GenerateFromPrompt(model.prompt));

    }
}