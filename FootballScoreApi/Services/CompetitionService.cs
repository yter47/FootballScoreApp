using FootballScoreApp.DTOs;
using FootballScoreApp.Services.IServices;
using Newtonsoft.Json;

namespace FootballScoreApp.Services
{
    public class CompetitionService : ICompetitionService
    {
        private readonly HttpClient _httpClient;

        public CompetitionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", "0dd0934d9bc24b1ab66e18fc098e288d");
        }

        public async Task<CompetitonsResponse> GetAvailableLeagues()
        {
            var response = await _httpClient.GetAsync($"https://api.football-data.org/v4/competitions/");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<CompetitonsResponse>(content);

                return data;
            }
            throw new HttpRequestException($"Anropet misslyckades, statuskod: {response.StatusCode}");
        }

        public async Task<Competiton> GetCompetitionByShortName(string shortName)
        {
            var response = await _httpClient.GetAsync($"https://api.football-data.org/v4/competitions/{shortName}");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Competiton>(content);

                return data;
            }
            throw new HttpRequestException($"Anropet misslyckades, statuskod: {response.StatusCode}");
        }

        public async Task<MatchesReponse> GetMatchesByCompetitionId(int id)
        {
            var response = await _httpClient.GetAsync($"https://api.football-data.org/v4/competitions/{id}/matches");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<MatchesReponse>(content);

                return data;
            }
            throw new HttpRequestException($"Anropet misslyckades, statuskod: {response.StatusCode}");
        }

        public async Task<StandingResponse> GetStandingsByCompetitionId(int id)
        {
            var response = await _httpClient.GetAsync($"https://api.football-data.org/v4/competitions/{id}/standings");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<StandingResponse>(content);

                return data;
            }
            throw new HttpRequestException($"Anropet misslyckades, statuskod: {response.StatusCode}");
        }
    }
}
