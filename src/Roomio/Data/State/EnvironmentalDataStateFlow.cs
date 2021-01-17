using System;
using System.Collections.Immutable;

using Rudder;

namespace Roomio.Data.State
{
    public class EnvironmentalDataStateFlow : IStateFlow<RoomioAppState>
    {
        public RoomioAppState Handle(RoomioAppState roomioAppState, object actionValue)
        {
            switch (actionValue)
            {
                case Actions.LoadEnvironmentalData.Request _:
                    return roomioAppState.With(state =>
                    {
                        state.IsFetching = true;
                    });

                case Actions.LoadEnvironmentalData.Success action:
                    return roomioAppState.With(state =>
                    {
                        state.IsFetching = false;
                        state.RoomData = action.Data.ToImmutableList();
                        state.LastFetched = DateTime.UtcNow;
                    });

                default:
                    return roomioAppState;
            }
        }
    }
}
