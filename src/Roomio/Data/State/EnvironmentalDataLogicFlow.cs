using System;
using System.Threading.Tasks;

using Rudder;

namespace Roomio.Data.State
{
    public class EnvironmentalDataLogicFlow : ILogicFlow
    {
        private readonly Store<RoomioAppState> _store;
        private readonly InfluxDbClient _client;

        public EnvironmentalDataLogicFlow(Store<RoomioAppState> store, InfluxDbClient client)
        {
            _store = store;
            _client = client;
        }

        public async Task OnNext(object action)
        {
            switch (action)
            {
                case Actions.LoadEnvironmentalData.Invoke _:
                    await LoadEnvironmentalData();
                    break;
            }
        }

        private async Task LoadEnvironmentalData()
        {
            await _store.Put(new Actions.LoadEnvironmentalData.Request());

            try
            {
                RoomData[] result = await _client.GetRuuviTagData();
                await _store.Put(new Actions.LoadEnvironmentalData.Success {Data = result});
            }
            catch (Exception ex)
            {
                await _store.Put(new Actions.LoadEnvironmentalData.Failure { ErrorMessage = ex.Message });

            }
        }
    }
}
