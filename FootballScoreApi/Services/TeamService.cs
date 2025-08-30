using FootballScoreApp.DTOs;
using FootballScoreApp.Services.IServices;
using Newtonsoft.Json;

namespace FootballScoreApp.Services
{
    public class TeamService : ITeamService
    {
        private readonly HttpClient _httpClient;
        private readonly string _authToken;
        public TeamService(HttpClient httpClient
            , IConfiguration configuration
            )
        {
            this._authToken = configuration["ApiSettings:AuthToken"];

            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", this._authToken);
        }

        public async Task<Team> GetTeamById(int id)
        {
            var response = await _httpClient.GetAsync($"https://api.football-data.org/v4/teams/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Team>(content);

                return data;
            }
            throw new HttpRequestException($"Anropet misslyckades, statuskod: {response.StatusCode}");
        }
    }
}
