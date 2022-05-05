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
    public class DepartmentsController : ApiController
    {
        private IDepartment iDepartments;

        public DepartmentsController(IDepartment iDepartments)
        {
            this.iDepartments = iDepartments;
        }

        public IEnumerable<Department> Get()
        {
            return iDepartments.GetDepartments();
        }

        public Department Get(int id)
        {
            return iDepartments.GetDepartmentbyId(id);
        }

        public HttpResponseMessage Post([FromBody] Department department)
        {
            try
            {
                Department objdepartment = iDepartments.SaveData(department);

                if(objdepartment == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error in Saving data");
                }
                else
                { 
                    var message = Request.CreateResponse(HttpStatusCode.Created, objdepartment);
                    message.Headers.Location = new Uri(Request.RequestUri + objdepartment.Id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody] Department department)
        {
            try
            {
                Department objDepartment = iDepartments.UpdateData(id, department);
                if (objDepartment == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Department with id = " + id.ToString() + " not found to update");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, objDepartment);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
