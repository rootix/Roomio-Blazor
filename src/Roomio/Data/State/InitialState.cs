using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

using Rudder;

namespace Roomio.Data.State
{
    public class InitialState : IInitialState<RoomioAppState>
    {
        public Task<RoomioAppState> GetInitialState()
        {
            var state = new RoomioAppState
            {
                IsFetching = false,
                RoomData = new List<RoomData>().ToImmutableList()
            };

            return Task.FromResult(state);
        }
    }
}
