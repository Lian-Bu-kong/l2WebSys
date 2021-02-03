using AkkaSys.Event;
using AkkaSys.Message;
using DummyAkkaNetWeb.ViewModel;
using Microsoft.AspNetCore.SignalR;

namespace DummyAkkaNetWeb.Hubs
{
    public class TrackingEventPusher : ITrackingEventPusher
    {
        private readonly IHubContext<TrackingHub> _trackHubContext;

        public TrackingEventPusher(IHubContext<TrackingHub> trackContext)
        {
            _trackHubContext = trackContext;
        }

        public void UpdateTrackingMap(PLCTrackMsg.TrackMap model)
        {
            var trkViewModel = new TrackingViewModel()
            {
                Uncoiler = model.Uncoiler,
                UncoilerSkid1 = model.UncoilerSkid1,
                UncoilerSkid2 = model.UncoilerSkid2,
                Recoiler = model.Recoiler,
                RecoilerSkid2 = model.RecoilerSkid2,
                RecoilerSkid1 = model.RecoilerSkid1,
                ActualRollForce = model.ActualRollForce,
                ActualElongation = model.ActualElongation,
                SetupRollForce = model.SetupRollForce,
                SetupElongation = model.SetupElongation
            };

            _trackHubContext.Clients.All.SendAsync("UpdateTrackingMap", trkViewModel);
        }
    }
}
