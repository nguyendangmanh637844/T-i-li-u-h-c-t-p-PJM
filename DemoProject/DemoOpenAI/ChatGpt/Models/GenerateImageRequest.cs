using OpenAI.Images;

namespace ChatGpt.Models
{
    public class GenerateImageRequest
    {
        public string Description { get; set; }
        public int NumberOfResult { get; set; }
        public ImageSize ImageSize { get; set; }
    }
}
