using System;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DataLayer;
using System.Data.SqlClient;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Models;

namespace BusinessLayer
{
    public interface IJobs
    {
        JobsResponse GetJobs(SearchJobReq request);
        JobData GetJobbyId(int id);
        Job SaveJob(Job job);
        Job UpdateJob(int id, Job job);
    }

    public class JobResults:IJobs
    {
        public JobsResponse GetJobs(SearchJobReq request)
        {
            using (masterEntities entities = new masterEntities())
            {                
                List<JobDetails> jobDetails = entities.Database.SqlQuery<JobDetails>("exec GetJobDetails @param1, @param2, @param3,@param4, @param5",
                    new SqlParameter("param1", request.searchString),
                    new SqlParameter("param2", request.pageNo),
                    new SqlParameter("param3", request.pageSize),
                    new SqlParameter("param4", request.locationId),
                    new SqlParameter("param5", request.departmentId)).ToList();
                
                JobsResponse jobResponse = new JobsResponse();
                jobResponse.data = new List<JobDetails>();
                
                jobResponse.data.AddRange(jobDetails);
                jobResponse.total = jobDetails.Count();

                return jobResponse;             
            }
        }

        public JobData GetJobbyId(int id)
        {
            using (masterEntities entities = new masterEntities())
            {
                var job = entities.Jobs.Where(e => e.Id == id).FirstOrDefault();

                JobData objJob = new JobData();
                objJob.Id = job.Id;
                objJob.Code = "Job" + "-" + job.Id;
                objJob.Title = job.Title;
                objJob.Description = job.Description;
                objJob.PostedDate = Convert.ToDateTime(job.PostedDate);
                objJob.ClosingDate = Convert.ToDateTime(job.ClosingDate);

                objJob.Department = new Department();
                objJob.Department.Id = Convert.ToInt16(job.DepartmentId);
                objJob.Department.Name = job.Department.Name;

                objJob.Location = new Location();
                objJob.Location.Id = Convert.ToInt16(job.LocationId);
                objJob.Location.Title = job.Location.Title;
                objJob.Location.City = job.Location.City;
                objJob.Location.Country = job.Location.Country;
                objJob.Location.State = job.Location.State;
                objJob.Location.Zip = job.Location.Zip;
                return objJob;
            }
        }

        public Job SaveJob(Job job)
        {
            try
            {
                using (masterEntities entities = new masterEntities())
                {
                    entities.Jobs.Add(job);
                    entities.SaveChanges();                    
                    return job;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Job UpdateJob(int id, Job job)
        {
            try
            {
                using (masterEntities entities = new masterEntities())
                {
                    var entity = entities.Jobs.Where(e => e.Id == id).FirstOrDefault();
                    if (entity == null)
                    {
                        return null;
                    }
                    else
                    {
                        entity.Description = job.Description;
                        entity.ClosingDate = job.ClosingDate;
                        entity.DepartmentId = job.DepartmentId;
                        entity.LocationId = job.LocationId;
                        entity.PostedDate = job.PostedDate;
                        entity.Title = job.Title;
                        entities.SaveChanges();                        
                        return entity;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}