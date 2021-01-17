using Vibrant.InfluxDB.Client;

namespace Roomio.Data
{
    public class RoomData
    {
        public string RoomName { get; set; }

        public RoomType Type { get; set; }

        [InfluxField("last")]
        public decimal Temperature { get; set; }

        [InfluxField("last_1")]
        public decimal Humidity { get; set; }

        [InfluxField("last_2")]
        public decimal Pressure { get; set; }

        [InfluxField("min")]
        public decimal MinTemperature { get; set; }

        [InfluxField("max")]
        public decimal MaxTemperature { get; set; }
    }
}
