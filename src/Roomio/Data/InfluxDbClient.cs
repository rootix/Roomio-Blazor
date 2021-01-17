using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using Roomio.Configuration;

using Vibrant.InfluxDB.Client;

namespace Roomio.Data
{
    public class InfluxDbClient
    {
        private readonly InfluxClient _influxClient;
        private readonly AppConfiguration _configuration;
        private readonly Lazy<string> _query;


        public InfluxDbClient(IOptions<AppConfiguration> options)
        {
            _configuration = options.Value;
            _influxClient = new InfluxClient(new Uri(_configuration.InfluxDbHost));
            _query = new Lazy<string>(BuildQuery);
        }

        public async Task<RoomData[]> GetRuuviTagData()
        {
            InfluxResultSet<RoomData> result = await _influxClient.ReadAsync<RoomData>(_configuration.InfluxDbName, _query.Value);
            return result.Results[0].Series.Select(MapToRoomData).ToArray();
        }
        private RoomData MapToRoomData(InfluxSeries<RoomData> serie)
        {
            RoomData roomData = serie.Rows[0];
            roomData.Temperature = RoundValue(roomData.Temperature);
            roomData.MinTemperature = RoundValue(roomData.MinTemperature);
            roomData.MaxTemperature = RoundValue(roomData.MaxTemperature);
            roomData.Humidity = RoundValue(roomData.Humidity);
            roomData.RoomName = serie.GroupedTags["name"].ToString();
            if (roomData.RoomName == "Aussen")
            {
                roomData.Type = RoomType.Outdoor;
            }

            return roomData;
        }

        private decimal RoundValue(decimal value)
        {
            return Math.Round(value, 1, MidpointRounding.AwayFromZero);
        }

        private string BuildQuery()
        {
            return $"SELECT MIN(temperature), MAX(temperature), LAST(temperature), LAST(humidity), LAST(pressure) FROM {_configuration.InfluxDbMeasurement} WHERE time > now() - 1d GROUP BY \"name\"";
        }
    }


}