namespace Roomio.Data.State
{
    public static class Actions
    {
        public static class LoadEnvironmentalData
        {
            public struct Invoke
            {
            }

            public struct Request
            {
            }

            public struct Success
            {
                public RoomData[] Data { get; set; }
            }

            public struct Failure
            {
                public string ErrorMessage { get; set; }
            }
        }
    }
}
