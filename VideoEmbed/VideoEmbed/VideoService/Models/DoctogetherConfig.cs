namespace VideoEmbed.VideoService.Models
{
    public class DoctogetherConfig 
    {
        public string AuthApiUrl { get; set; } = "https://auth.doctogether.com";
        public string AuthApiClientSecret { get; set; } = "KRfMKT1jT0x64RWdEWMAz2cDFNv1fc";
        public string AuthApiClientId { get; set; } = "VideoDemo";
        public string SignRequestApiUrl { get; set; } = "https://api.doctogether.com";
    }
}