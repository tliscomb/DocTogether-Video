namespace VideoEmbed.VideoService.Models
{
    public class VideoRoomAccessInfo
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public string id { get; set; } = string.Empty;
        public string videoServerUrl { get; set; } = string.Empty;
        public string socketUrl { get; set; } = string.Empty;
        public string socketToken { get; set; } = string.Empty;
        public string janusToken { get; set; } = string.Empty;
    }
}