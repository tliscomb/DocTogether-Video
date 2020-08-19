using System;
using System.Text.RegularExpressions;
using IdentityModel;
using LazyCache;
using RestSharp;
using RestSharp.Authenticators;
using VideoEmbed.VideoService.Models;

namespace VideoEmbed.VideoService
{
    internal class VideoConferenceService
    {
        internal readonly DoctogetherConfig Config;
        private readonly IAppCache _cache = new CachingService();

        internal VideoConferenceService()
        {
            //load or pass in configs
            Config = new DoctogetherConfig();
        }

        private AccessTokenResponse FetchAuthTokenFromServer()
        {
            var restClient = new RestClient(Config.AuthApiUrl);
            var request = new RestRequest("connect/token", Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("client_id", Config.AuthApiClientId);
            request.AddParameter("client_secret", Config.AuthApiClientSecret);
            request.AddParameter("grant_type", OidcConstants.GrantTypes.ClientCredentials);

            var response = restClient.Execute<AccessTokenResponse>(request);
            return response.Data;
        }

        internal string GetAuthToken()
        {
            const string key = nameof(GetAuthToken);
            var value = _cache.Get<AccessTokenResponse>(key);
            if (value == null)
            {
                var tokenData = FetchAuthTokenFromServer();
                _cache.Add(key, tokenData, DateTimeOffset.Now.AddSeconds(tokenData.expires_in - 60));
            }

            return _cache.Get<AccessTokenResponse>(key).access_token;
        }

        internal IRestClient GetRestClient()
        {
            var restClient = new RestClient(Config.SignRequestApiUrl)
            {
                Authenticator = new JwtAuthenticator(GetAuthToken())
            };

            return restClient;
        }


        public VideoRoomAccessInfo GetConferenceInfo(string roomId)
        {
            var regEx = new Regex("[^A-Za-z0-9_]");
            var key = regEx.Replace(roomId, "");
            var client = GetRestClient();
            var request = new RestRequest($"/video-rooms/{key}", Method.GET);

            var response = client.Execute<VideoRoomAccessInfo>(request);
            return response.Data;
        }
    }
}