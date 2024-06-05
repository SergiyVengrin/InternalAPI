namespace InternalAPI.Models.ChatCompletionModels;

public class ChoiceModel
{
    public int index { get; set; }
    public MessageModel message { get; set; }
    public string finish_reason { get; set; }
}
