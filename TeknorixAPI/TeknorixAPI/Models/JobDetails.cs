using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using DataLayer;
namespace BusinessLayer.Models
{
    [JsonObject(Title = "JobDetails")]
    public class JobsResponse
    {
        [JsonProperty]
        public int total { get; set; }
                
        //[JsonArray]
        public List<JobDetails> data { get; set; }
    }
        
        [JsonObject(Title = "JobDetails")]
        public class JobDetails
        {
            [JsonProperty]
            int id { get; set; }
            [JsonProperty]
            string code { get; set; }
            [XmlElement]
            [JsonProperty]
            string title { get; set; }
            [XmlElement]
            [JsonProperty]
            string location { get; set; }
            [XmlElement]
            [JsonProperty]
            string department { get; set; }
            [XmlElement]
            [JsonProperty]
            DateTime postedDate { get; set; }
            [XmlElement]
            [JsonProperty]
            DateTime closingDate { get; set; }
        }

        public class JobData
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime ClosingDate { get; set; }
            public DateTime PostedDate { get; set; }

            public Department Department { get; set; }
            public Location Location { get; set; }
        }

        public class SearchJobReq
        {
            public string searchString { get; set; }
            public int pageNo { get; set; }
            public int pageSize { get; set; }
            public int locationId { get; set; }
            public int departmentId { get; set; }
        }    
}