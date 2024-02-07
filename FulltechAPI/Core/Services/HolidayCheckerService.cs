using FulltechAPI.Core.Entities;

using System.Globalization;
using System.Text.Json;

namespace FulltechAPI.Core.Services
{
    public class HolidayCheckerService
    {
        private readonly HttpClient _httpClient;

        public HolidayCheckerService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> IsWorkingDay(DateTime date)
        {

            var dataAlternativa = 2023;
            // Obter a lista de feriados do ano atual
            var holidays = await GetHolidaysAsync(dataAlternativa);

            // Verificar se a data da transferência não é um feriado
            foreach (var holiday in holidays)
            {
                if (holiday.Date.Date == date.Date)
                {
                    return false;
                }
            }

            // Verificar se a data da transferência é um sábado ou domingo
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }

            return true;
        }

        private async Task<Holiday[]> GetHolidaysAsync(int year)
        {
            var response = await _httpClient.GetAsync($"https://brasilapi.com.br/api/feriados/v1/{year}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Falha ao obter feriados.");
            }

            var holidaysJson = await response.Content.ReadAsStringAsync();
            var holidays = JsonSerializer.Deserialize<Holiday[]>(holidaysJson);

            return holidays;
        }
    }
}
