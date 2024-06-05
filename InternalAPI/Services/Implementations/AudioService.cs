using InternalAPI.Services.Interfaces;

namespace InternalAPI.Services;

public class AudioService:IAudioService
{
    private readonly IOpenAIHttpClient _openAiHttpClient;

    public AudioService(IOpenAIHttpClient openAiHttpClient)
    {
        _openAiHttpClient = openAiHttpClient;
    }

    public async Task<string> AudioTranscription(string audioFilePath)
    {
        string transcriptedText = await _openAiHttpClient.AudioTranscription(audioFilePath);

        if (!string.IsNullOrEmpty(transcriptedText))
        {
            return transcriptedText;
        }

        return string.Empty;
    }
}