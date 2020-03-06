using System.Collections.Generic;
using System.Linq;
using Muapise.Common.Utils;

namespace Muapise.Utils
{
    public sealed class WorkerEndPoints
    {
        private static WorkerEndPoints instance;
        private readonly List<string> _endpointsList = new List<string>();
        private string _lastEndPointUsed = string.Empty;

        private WorkerEndPoints() { }

        public static WorkerEndPoints Instance
        {
            get
            {
                if (instance == null) { instance = new WorkerEndPoints(); }

                return instance;
            }
        }

        public void SetPoolEndPointsValueOne(List<Dictionary<string, string>> endpoints)
        {
            _endpointsList.Clear();
            foreach (var endpoint in endpoints)
            foreach (var value in endpoint.Values)
                _endpointsList.Add(value);
        }

        public string GetFirstEndPoint()
        {
            _lastEndPointUsed = _endpointsList.FirstOrDefault();
            return _lastEndPointUsed;
        }

        public string GetNextAvailableEndpoint()
        {
            var next = _endpointsList.FirstOrDefault(t => t != _lastEndPointUsed);
            if (next == null) { next = string.Empty; }
            else
            {
                if (NetUtils.IsValidIpAddress(next)) { _lastEndPointUsed = next; }
            }
            return _lastEndPointUsed;
        }
    }
}