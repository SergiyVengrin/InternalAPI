using InternalAPI.Services.Interfaces;

namespace InternalAPI.Services;

public class ImageService : IImageService
{
    private readonly IOpenAIHttpClient _openAiHttpClient;

    public ImageService(IOpenAIHttpClient openAiHttpClient)
    {
        _openAiHttpClient = openAiHttpClient;
    }

    public async Task<string> GenerateFromPrompt(string prompt)
    {
        return await _openAiHttpClient.ImagesGenerations(prompt);
    }
}