namespace VideoEmbed.VideoService.Models
{
    public class AccessTokenResponse
    {
        /// <summary>
        /// The access token usually jwt.
        /// </summary>
        public string access_token { get; set; } = string.Empty;
        /// <summary>
        /// The time the token will expire in seconds from now.
        /// </summary>
        public int expires_in { get; set; }
        /// <summary>
        /// The token type, usually jwt
        /// </summary>
        public string token_type { get; set; } = string.Empty;
        /// <summary>
        /// The token's scope.
        /// </summary>
        public string scope { get; set; } = string.Empty;
    }
}