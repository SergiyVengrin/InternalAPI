namespace InternalAPI.Models.ImageGenerationModels;

public class ImageGenerationRequestModel
{
    public string model { get; set; }
    public string prompt { get; set; }
    public int n { get; set; }
    public string size { get; set; }
}