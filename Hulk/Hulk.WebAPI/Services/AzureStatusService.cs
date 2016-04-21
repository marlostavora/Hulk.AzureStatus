using Hangfire;
using Hulk.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulk.WebAPI.Services
{
    public static class AzureStatusService
    {
        public static void ProcessAzureStatus()
        {
            AzureStatusRepository repository = new AzureStatusRepository();
            repository.ProcessAzureStatus();
        }
    }
}
