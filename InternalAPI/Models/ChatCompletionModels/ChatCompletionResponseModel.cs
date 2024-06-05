namespace InternalAPI.Models.ChatCompletionModels;

public class ChatCompletionResponseModel
{
    public string id { get; set; }
    public string @object { get; set; }
    public long created { get; set; }
    public string model { get; set; }
    public List<ChoiceModel> choices { get; set; }
    public UsageModel usage { get; set; }
    public string system_fingerprint { get; set; }
}