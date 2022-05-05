using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataLayer;
using BusinessLayer;
using BusinessLayer.Models;

namespace BusinessLayer.Controllers
{
    public class JobsController : ApiController
    {
        private IJobs iJobs;

        public JobsController(IJobs iJobs)
        {
            this.iJobs = iJobs;
        }

        public JobsResponse Get([FromBody] SearchJobReq request)
        {
            return iJobs.GetJobs(request);
        }

        public JobData Get(int id)
        {
            JobData objJobData = iJobs.GetJobbyId(id);
            return objJobData;
        }

        public HttpResponseMessage Post([FromBody] Job job)
        {
            Job objJob = iJobs.SaveJob(job);
            if (objJob == null)
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error in Saving data");
            else
            { 
                var message = Request.CreateResponse(HttpStatusCode.Created, objJob);
                message.Headers.Location = new Uri(Request.RequestUri + objJob.Id.ToString());
                return message;
            }
        }

        public HttpResponseMessage Put(int id, [FromBody] Job job)
        {
            Job objJob = iJobs.UpdateJob(id,job);
            if(objJob == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Job with id = " + id.ToString() + " not found to update");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, objJob);
            }
        }
    }
}
