namespace InternalAPI.Models.ChatCompletionModels;

public class ChatCompletionRequestModel
{
    public string model { get; set; }
    public List<MessageModel> messages { get; set; } 
}