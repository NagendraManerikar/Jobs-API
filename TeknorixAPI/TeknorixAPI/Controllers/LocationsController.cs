using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataLayer;
using BusinessLayer;

namespace TeknoRixJobsAPI.Controllers
{
    public class LocationsController : ApiController
    {
        private ILocations iLocations;

        public LocationsController(ILocations ilocations)
        {
            this.iLocations = ilocations;
        }

        public IEnumerable<Location> Get()
        {
            return iLocations.GetLocations();
        }

        public Location Get(int id)
        {
            return iLocations.GetLocationbyId(id);
        }

        public HttpResponseMessage Post([FromBody] Location location)
        {
            try
            {
                Location objLocation = iLocations.SaveData(location);
                
                if(objLocation == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error in Saving data");
                }
                else
                { 
                    var message = Request.CreateResponse(HttpStatusCode.Created, objLocation);
                    message.Headers.Location = new Uri(Request.RequestUri + objLocation.Id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody] Location location)
        {
            try
            {   
                Location objlocation = iLocations.UpdateData(id, location);
                if(objlocation == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Location with id = " + id.ToString() + " not found to update");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, objlocation);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
