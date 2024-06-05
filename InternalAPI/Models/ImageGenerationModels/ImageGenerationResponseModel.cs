using System.Numerics;

namespace InternalAPI.Models.ImageGenerationModels;

public class ImageGenerationResponseModel
{
    public long created { get; set; }
    public List<ImageGenerationDataModel> data { get; set; }
}