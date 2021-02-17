using DummyAkkaNetWeb.ViewModel;
using System.Threading.Tasks;

namespace DummyAkkaNetWeb.Hubs.Tracking
{
    public interface ITrackingHub
    {
        Task UpdateTrackingMap(TrackingViewModel model);
    }
}
