using System;
using System.Collections.Immutable;

using Rudder;

namespace Roomio.Data.State
{
    public struct EnvironmentalDataState : IStateMap<RoomioAppState, EnvironmentalDataState>
    {
        public ImmutableList<RoomData> RoomData { get; set; }

        public bool IsFetching { get; set; }

        public DateTime? LastFetched { get; set; }

        public EnvironmentalDataState MapState(RoomioAppState state) => new EnvironmentalDataState
        {
            RoomData = state.RoomData,
            IsFetching = state.IsFetching,
            LastFetched = state.LastFetched
        };
    }
}
