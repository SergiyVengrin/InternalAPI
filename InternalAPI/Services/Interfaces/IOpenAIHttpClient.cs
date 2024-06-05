using InternalAPI.Models;
using InternalAPI.Models.ChatCompletionModels;

namespace InternalAPI.Services.Interfaces;

public interface IOpenAIHttpClient
{
    Task<string> ChatCompletions(IEnumerable<MessageModel> messages);

    Task<string> ImagesGenerations(string prompt);

    Task<string> AudioTranscription(string audioFilePath);
}