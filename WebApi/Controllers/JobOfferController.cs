using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades;

namespace WebApi.Controllers
{
    public class JobOfferController : ApiController
    {
        public JobOfferFacade JobOfferFacade { get; set; }

        // GET api/joboffer
        public async Task<IEnumerable<JobOfferDto>> Get()
        {
            return (await JobOfferFacade.GetAllOffers()).Items;
        }

        // GET api/joboffer/1
        public async Task<JobOfferDto> Get(int id)
        {
            return (await JobOfferFacade.GetOffer(id));
        }
    }
}