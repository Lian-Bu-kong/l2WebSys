namespace AkkaSys.Message
{
    public class PLCTrackMsg
    {
        public class TrackMap
        {
            public string Uncoiler { get; set; } = string.Empty;

            public string UncoilerSkid1 { get; set; } = string.Empty;

            public string UncoilerSkid2 { get; set; } = string.Empty;

            public string Recoiler { get; set; } = string.Empty;

            public string RecoilerSkid2 { get; set; } = string.Empty;

            public string RecoilerSkid1 { get; set; } = string.Empty;

            public string ActualRollForce { get; set; } = string.Empty;

            public string ActualElongation { get; set; } = string.Empty;

            public string SetupRollForce { get; set; } = string.Empty;

            public string SetupElongation { get; set; } = string.Empty;
        }
    }
}
