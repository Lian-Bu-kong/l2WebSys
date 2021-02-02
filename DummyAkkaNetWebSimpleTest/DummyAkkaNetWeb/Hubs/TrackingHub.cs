using Akka.Actor;
using AkkaBase;
using AkkaSys.PLC;
using DummyAkkaNetWeb.ViewModel;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace DummyAkkaNetWeb.Hubs
{
    /// <summary>
    /// Author : ICSC 豪霆
    /// Desc : TrackingHub (Actor與前端UI溝通)
    /// </summary>
    public class TrackingHub : Hub
    {
        //private readonly ISysAkkaManager _akkaManager;

        //public TrackingHub(ISysAkkaManager akkaManager)
        //{
        //    _akkaManager = akkaManager;
        //}

        public async Task Input(int l1_switch, float setup_rollForce, float setup_elongation)
        {
            //_akkaManager.GetActor(nameof(PLCSndEdit)).Tell("tracking");

            //  Test
            var model = new TrackingViewModel()
            {
                UncoilerSkid2 = "",
                UncoilerSkid1 = "",
                Uncoiler = "",
                Recoiler = "",
                RecoilerSkid2 = "",
                RecoilerSkid1 = "",
                ActualRollForce = (setup_rollForce * 1.1).ToString(),
                ActualElongation = (setup_elongation * 1.1).ToString(),
                SetupRollForce = setup_rollForce.ToString(),
                SetupElongation = setup_elongation.ToString()
            };
            await Clients.All.SendAsync("UpdateTrackingMap", model);
        }

        //public async Task UpdateTrackingMap(TrackingViewModel model)
        //{
        //    await Clients.All.SendAsync("UpdateTrackingMap", model);
        //}
    }
}
