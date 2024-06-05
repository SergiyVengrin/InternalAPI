using InternalAPI.Models;
using InternalAPI.Models.ChatCompletionModels;
using InternalAPI.Services.Interfaces;

namespace InternalAPI.Services;

public class ChatService : IChatService
{
    private readonly IOpenAIHttpClient _openAiHttpClient;

    public ChatService(IOpenAIHttpClient openAiHttpClient)
    {
        _openAiHttpClient = openAiHttpClient;
    }

    public async Task<string> ChatCompletion(IEnumerable<MessageModel> messages)
    {
        return await _openAiHttpClient.ChatCompletions(messages);
    }
}