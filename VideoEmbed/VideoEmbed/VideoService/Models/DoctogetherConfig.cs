namespace VideoEmbed.VideoService.Models
{
    public class DoctogetherConfig //todo:  Add this to config.
    {
        public string AuthApiUrl { get; set; } = "https://auth.doctogether.com";
        public string AuthApiClientSecret { get; set; } = "<Set Client Secret>";
        public string AuthApiClientId { get; set; } = "<Set Client Id>";

        public string SignRequestApiUrl { get; set; } = "https://api.doctogether.com";
    }
}