using Hulk.Model;
using Hulk.Repository.Contracts;
using Hulk.WebAPI.Models;
using Hulk.WebAPI.Services;
using Hulk.WebAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Hulk.WebAPI.Controllers
{
    [Authorize()]
    public class HomeController : ApiControllerBase
    {
        public HomeController(IHulkUow uow)
        {
            Uow = uow;
        }

        [ResponseType(typeof(ServiceStatus))]
        public IHttpActionResult GetServiceStatus()
        {
            try
            {
                var servicesStatus = Uow.ServicesStatus.GetAll();
                var serviceStatusVM = ConvertServiceStatusToServiceStatusVM(servicesStatus.ToList());
                var serviceStatusComplete = CompleteRegions(serviceStatusVM);
                return Ok(serviceStatusComplete);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private ServiceStatusVM ConvertServiceStatusToServiceStatusVM(List<ServiceStatus> servicesStatus)
        {
            ServiceStatusVM serviceStatusVM = new ServiceStatusVM();

            //TODO: usar linq para agrupar

            foreach (ServiceStatus service in servicesStatus)
            {
                if (!serviceStatusVM.Regions.Exists(r => r.Id == service.RegionId))
                {
                    serviceStatusVM.Regions.Add(new RegionVM()
                    {
                        Id = service.RegionId,
                        Name = service.Region.Name
                    });
                }

                ServiceVM serviceVM;
                if (!serviceStatusVM.Services.Any(s => s.Id == service.ServiceId))
                {
                    serviceVM = new ServiceVM()
                    {
                        Id = service.ServiceId,
                        Name = service.Service.Name,
                    };
                    serviceStatusVM.Services.Add(serviceVM);
                }
                else
                {
                    serviceVM = serviceStatusVM.Services.First(s => s.Id == service.ServiceId);
                }

                serviceVM.Regions.Add(new RegionStatusVM()
                {
                    RegionId = service.RegionId,
                    Name = service.Region.Name,
                    Status = service.Status.ToString()
                });
            }

            return serviceStatusVM;
        }

        private ServiceStatusVM CompleteRegions(ServiceStatusVM serviceStatusVM)
        {
            foreach (RegionVM region in serviceStatusVM.Regions)
            {
                foreach (ServiceVM service in serviceStatusVM.Services)
                {
                    if (!service.Regions.Any(r => r.RegionId == region.Id))
                    {
                        service.Regions.Add(new RegionStatusVM()
                        {
                            RegionId = region.Id,
                            Name = region.Name,
                            Status = string.Empty
                        });
                    }
                }
            }

            return serviceStatusVM;
        }


        public IHttpActionResult SendEmail(EmailData emailData)
        {
            try
            {
                if(emailData == null)
                {
                    return BadRequest("Service Status cannot be null");
                }

                //TODO: get informations in database
                string host = "smtp.gmail.com";
                string emailFrom = "youremail@domain.com";
                string user = "youremail@domain.com";
                string password = "yourpassword";
                int port = 587;
                string subject = GetEmailSubject();
                string body = GetEmailBody(String.IsNullOrEmpty(emailData.Message) ? "" : emailData.Message);
                //

                EmailService.SendEmail(host, emailFrom, emailData.Email, subject, body, user, password, port);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private string GetEmailSubject()
        {
            return "See Azure Status by Marlos Távora";
        }

        private string GetEmailBody(string message)
        {
            return "Hi," + 
                    "</br>" +
                    message +
                    "</br>" +
                    "This email was sent from the solution that shows the status of Microsoft Azure services developed by Marlos Távora" +
                    "</br>" +
                    "Follow the link below for more details:" +
                    "</br>" +
                    "https://github.com/marlostavora";
        }
    }
}
