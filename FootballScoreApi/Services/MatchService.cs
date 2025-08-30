using FootballScoreApp.DTOs;
using FootballScoreApp.Services.IServices;
using Newtonsoft.Json;

namespace FootballScoreApp.Services
{
    public class MatchService : IMatchService
    {
        private readonly HttpClient _httpClient;
        private readonly string _authToken;

        public MatchService(HttpClient httpClient
            , IConfiguration configuration
            )
        {
            this._authToken = configuration["ApiSettings:AuthToken"];

            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", this._authToken);
        }

        public async Task<MatchesReponse> GetRecentMatches()
        {
            var response = await _httpClient.GetAsync("https://api.football-data.org/v4/matches");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<MatchesReponse>(content);

                return data;
            }
            throw new HttpRequestException($"Anropet misslyckades, statuskod: {response.StatusCode}");
        }
        
        public async Task<Match> GetMatchById(int id)
        {
            var response = await _httpClient.GetAsync($"https://api.football-data.org/v4/matches/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Match>(content);

                return data;
            }
            throw new HttpRequestException($"Anropet misslyckades, statuskod: {response.StatusCode}");
        }
    }
}
