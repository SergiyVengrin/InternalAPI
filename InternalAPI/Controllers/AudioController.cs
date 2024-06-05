using InternalAPI.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace InternalAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[EnableCors("AllowSpecificOrigin")]
public class AudioController : ControllerBase
{
    private readonly IAudioService _audioService;
    private readonly IHostingEnvironment _hostingEnvironment;


    public AudioController(IAudioService audioService, IHostingEnvironment hostingEnvironment)
    {
        _audioService = audioService;
        _hostingEnvironment = hostingEnvironment;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> UploadAudio(IFormFile audioFile)
    {
        if (audioFile.Length == 0)
        {
            return BadRequest("Please provide audio file.");
        }

        try
        {
            string uploadsFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads");
            string filePath = Path.Combine(uploadsFolderPath, audioFile.FileName);

            using (FileStream? stream = System.IO.File.Create(filePath))
            {
                await audioFile.CopyToAsync(stream);
            }

            return Ok(await _audioService.AudioTranscription(filePath));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}