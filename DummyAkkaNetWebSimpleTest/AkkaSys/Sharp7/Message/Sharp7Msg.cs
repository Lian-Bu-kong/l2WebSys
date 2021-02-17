namespace AkkaSys.Message
{
    public class Sharp7Msg
    {
        public class TrackMap
        {
            // 位置
            public string Uncoiler { get; set; } = string.Empty;

            public string UncoilerSkid1 { get; set; } = string.Empty;

            public string UncoilerSkid2 { get; set; } = string.Empty;

            public string Recoiler { get; set; } = string.Empty;

            public string RecoilerSkid2 { get; set; } = string.Empty;

            public string RecoilerSkid1 { get; set; } = string.Empty;

            // 參數
            public string ActualRollForce { get; set; } = string.Empty;

            public string ActualElongation { get; set; } = string.Empty;

            public string SetupRollForce { get; set; } = string.Empty;

            public string SetupElongation { get; set; } = string.Empty;
        }

        public class Connecttion
        {
        }

        public class Switch
        {
            public bool Open { get; set; }
        }

        public class TimerModel
        {

        }
    }
}
