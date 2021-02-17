using static AkkaSys.Message.PLCTrackMsg;

namespace AkkaSys.Event
{
    public interface ITrackingEventPusher
    {
        void UpdateTrackingMap(TrackMap model);
    }
}
