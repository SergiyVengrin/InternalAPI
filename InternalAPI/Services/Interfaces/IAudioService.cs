namespace InternalAPI.Services.Interfaces;

public interface IAudioService
{
    Task<string> AudioTranscription(string audioFilePath);
}