using Hulk.Repository.Contracts;
using Hulk.Repository.Helpers;
using Hulk.Repository.Helpers.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulk.Repository
{
    public class AzureStatusRepository : IAzureStatusRepository
    {
        HulkUow Uow;

        public AzureStatusRepository()
        {
            Uow = new HulkUow(new RepositoryProvider(new RepositoryFactories()));
        }

        public void ProcessAzureStatus()
        {
            try
            {
                AzureStatus azureStatus = AzureStatusEngine.GetAzureStatus();
                
                foreach(Service service in azureStatus.Services)
                {
                    Model.Service serviceDb = GetServiceDbByServiceId(service.Id);

                    if(serviceDb == null)
                    {
                        serviceDb = CreateServiceDb(service);
                    }

                    foreach(Region region in service.Regions)
                    {
                        Model.Region regionDb = GetRegionDbByRegionId(region.Id);

                        if(regionDb == null)
                        {
                            regionDb = CreateRegionDb(region);
                        }

                        Model.ServiceStatus serviceStatusDb = GetServiceStatusByServiceIdandRegionId(service, region);

                        if(serviceStatusDb == null)
                        {
                            serviceStatusDb = CreateServiceStatus(serviceDb, regionDb, (Model.Status)Enum.Parse(typeof(Model.Status), region.Status));
                        }
                        else
                        {
                            UpdateServiceStatus(serviceStatusDb, (Model.Status)Enum.Parse(typeof(Model.Status), region.Status));
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private Model.ServiceStatus GetServiceStatusByServiceIdandRegionId(Service service, Region region)
        {
            return Uow.ServicesStatus.GetAll().FirstOrDefault(s => s.Service.ServiceId == service.Id && s.Region.RegionId == region.Id);
        }

        private Model.ServiceStatus CreateServiceStatus(Model.Service serviceDb, Model.Region regionDb, Model.Status status)
        {
            Model.ServiceStatus serviceStatusDb = new Model.ServiceStatus()
            {
                Service = serviceDb,
                Region = regionDb,
                Status = status,
                DateUpdate = DateTime.UtcNow
            };
            Uow.ServicesStatus.Add(serviceStatusDb);
            Uow.Commit();
            return serviceStatusDb;
        }   
        
        private void UpdateServiceStatus(Model.ServiceStatus serviceStatusDb, Model.Status status)
        {
            serviceStatusDb.Status = status;
            serviceStatusDb.DateUpdate = DateTime.UtcNow;
            Uow.ServicesStatus.Update(serviceStatusDb);
            Uow.Commit();
        }

        private Model.Service GetServiceDbByServiceId(int serviceId)
        {
            return Uow.Services.GetAll().FirstOrDefault(s => s.ServiceId == serviceId);
        }

        private Model.Service CreateServiceDb(Service service)
        {
            Model.Service newServiceDb = new Model.Service()
            {
                ServiceId = service.Id,
                Name = service.Name
            };
            Uow.Services.Add(newServiceDb);
            Uow.Commit();
            return newServiceDb;
        }

        private Model.Region GetRegionDbByRegionId(int modelId)
        {
            return Uow.Regions.GetAll().FirstOrDefault(r => r.RegionId == modelId);
        }

        private Model.Region CreateRegionDb(Region region)
        {
            Model.Region newRegionDb = new Model.Region()
            {
                RegionId = region.Id,
                Name = region.Name
            };
            Uow.Regions.Add(newRegionDb);
            Uow.Commit();
            return newRegionDb;
        }
    }
}
