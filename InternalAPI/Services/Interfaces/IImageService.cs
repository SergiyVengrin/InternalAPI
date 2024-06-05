namespace InternalAPI.Services.Interfaces;

public interface IImageService
{
    Task<string> GenerateFromPrompt(string prompt);
}