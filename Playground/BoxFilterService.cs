using System.Collections.Generic;
using System.Linq;

namespace Playground
{
    public class BoxFilterService : IBoxFilterService
    {
        private readonly IEnumerable<Box> boxes;
        private readonly IAuthorizationService autzService;

        IEnumerable<Box> IBoxFilterService.FilterCustomBoxes(params BoxConfig[] boxConfigs)
        {
            return boxes.GetAllCustomBoxes(boxConfigs).Select(c => autzService.IsAuthorized(c) ? c : null);
        }

        public BoxFilterService(IEnumerable<Box> boxes, IAuthorizationService autzService)
        {
            this.boxes = boxes;
            this.autzService = autzService;
        }
    }

    public interface IBoxFilterService
    {
        IEnumerable<Box> FilterCustomBoxes(params BoxConfig[] boxConfigs);
    }

    public interface IAuthorizationService
    {
        bool IsAuthorized(Box boxUnderTest);
    }
}