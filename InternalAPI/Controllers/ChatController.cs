using InternalAPI.Models;
using InternalAPI.Models.ChatCompletionModels;
using InternalAPI.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace InternalAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[EnableCors("AllowSpecificOrigin")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;
    
    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }
    
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> ChatCompletion(List<MessageModel> messages)
    {
        if (!messages.Any())
        {
            return BadRequest("Invalid data.");
        }

        return Ok(await _chatService.ChatCompletion(messages));

    }
}