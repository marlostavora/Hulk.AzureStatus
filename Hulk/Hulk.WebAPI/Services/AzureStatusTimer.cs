using Hulk.WebAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hulk.WebAPI.Services
{
    public static class AzureStatusTimer
    {
        private static readonly Timer _timer = new Timer(OnTimerElapsed);
        private static readonly JobHost _jobHost = new JobHost();

        public static void Start()
        {
            _timer.Change(TimeSpan.Zero, TimeSpan.FromMilliseconds(900000));//15 minutes
        }

        private static void OnTimerElapsed(object sender)
        {
            _jobHost.DoWork(() => { AzureStatusService.ProcessAzureStatus(); });
        }
    }
}
