using System;
using System.Collections.Immutable;

namespace Roomio.Data.State
{
    public class RoomioAppState
    {
        public ImmutableList<RoomData> RoomData { get; set; }

        public bool IsFetching { get; set; }

        public DateTime? LastFetched { get; set; }
    }
}
