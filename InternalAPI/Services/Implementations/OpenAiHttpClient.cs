using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using InternalAPI.Models;
using InternalAPI.Models.ChatCompletionModels;
using InternalAPI.Models.ImageGenerationModels;
using InternalAPI.Services.Interfaces;

namespace InternalAPI.Services;

public class OpenAiHttpClient : IOpenAIHttpClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    private readonly string _apiToken;
    
    private readonly string _chatCompletionUrl;
    private readonly string _chatCompletionModel;

    private readonly string _imageGenerationUrl;
    private readonly string _imageGenerationModel;
    private readonly string _imageGenerationSize;
    private readonly int _imageGenerationN;

    private readonly string _audioTranscriptionUrl;
    private readonly string _audioTranscriptionModel;

    public OpenAiHttpClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        
        _apiToken = configuration["OpenAI:ApiToken"];
        
        _chatCompletionUrl = configuration["OpenAI:ChatCompletion:URL"];
        _chatCompletionModel = configuration["OpenAI:ChatCompletion:Model"];

        _imageGenerationUrl = configuration["OpenAI:ImageGeneration:URL"];
        _imageGenerationModel = configuration["OpenAI:ImageGeneration:Model"];
        _imageGenerationSize = configuration["OpenAI:ImageGeneration:Size"];
        _imageGenerationN = Convert.ToInt32(configuration["OpenAI:ImageGeneration:N"]);
        
        _audioTranscriptionUrl = configuration["OpenAI:AudioTranscription:URL"];
        _audioTranscriptionModel = configuration["OpenAI:AudioTranscription:Model"];
    }


    public async Task<string> ChatCompletions(IEnumerable<MessageModel> messages)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiToken);

        ChatCompletionRequestModel requestContent = new ChatCompletionRequestModel()
        {
            model = _chatCompletionModel,
            messages = messages.ToList()
        };

        StringContent content = new StringContent(JsonSerializer.Serialize(requestContent), Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync(_chatCompletionUrl, content);
        response.EnsureSuccessStatusCode();

        string? responseBody = await response.Content.ReadAsStringAsync();
        ChatCompletionResponseModel? chatCompletionResponse = JsonSerializer.Deserialize<ChatCompletionResponseModel>(responseBody);


        return chatCompletionResponse?.choices[0]?.message?.content ?? string.Empty;
    }

    public async Task<string> ImagesGenerations(string prompt)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiToken);

        ImageGenerationRequestModel requestContent = new ImageGenerationRequestModel()
        {
            model = _imageGenerationModel,
            prompt = prompt,
            n = _imageGenerationN,
            size = _imageGenerationSize
        };
        
        StringContent content = new StringContent(JsonSerializer.Serialize(requestContent), Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await client.PostAsync(_imageGenerationUrl, content);
        response.EnsureSuccessStatusCode();

        string? responseBody = await response.Content.ReadAsStringAsync();
        ImageGenerationResponseModel? imageGenerationResponse = JsonSerializer.Deserialize<ImageGenerationResponseModel>(responseBody);

        return imageGenerationResponse?.data[0]?.url ?? string.Empty;
    }

    public async Task<string> AudioTranscription(string audioFilePath)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiToken);

        using var multipartContent = new MultipartFormDataContent();
        using var fileStream = System.IO.File.OpenRead(audioFilePath);
        using var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("audio/mpeg");
        
        
        multipartContent.Add(fileContent, "file", System.IO.Path.GetFileName(audioFilePath));
        multipartContent.Add(new StringContent(_audioTranscriptionModel), "model");
        
        HttpResponseMessage response = await client.PostAsync(_audioTranscriptionUrl, multipartContent);
        response.EnsureSuccessStatusCode();

        string? responseBody = await response.Content.ReadAsStringAsync();
        AudioTranscriptionResponseModel? audioTranscriptionResponse = JsonSerializer.Deserialize<AudioTranscriptionResponseModel>(responseBody);

        return audioTranscriptionResponse?.text ?? string.Empty;
    } 
}