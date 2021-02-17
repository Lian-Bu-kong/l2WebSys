using static AkkaSys.Message.Sharp7Msg;

namespace AkkaSys.Event
{
    public interface ITrackingEventPusher
    {
        void UpdateTrackingMap(TrackMap model);
    }
}
