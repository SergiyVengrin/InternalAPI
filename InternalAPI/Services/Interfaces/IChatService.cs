using InternalAPI.Models;
using InternalAPI.Models.ChatCompletionModels;

namespace InternalAPI.Services.Interfaces;

public interface IChatService
{
    Task<string> ChatCompletion(IEnumerable<MessageModel> messages);
}