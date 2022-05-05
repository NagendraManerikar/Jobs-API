using System;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DataLayer;
using System.Net.Http;
using System.Web.Http;

namespace BusinessLayer
{
    public interface ILocations
    {
        IEnumerable<Location> GetLocations();
        Location GetLocationbyId(int Id);
        Location SaveData(Location location);
        Location UpdateData(int id, Location location);
    }

    public class LocationResults : ILocations
    {     
        public IEnumerable<Location> GetLocations()
        {
            using (masterEntities entities = new masterEntities())
            {
                var locations = entities.Locations.ToList();
                return locations;
            }
        }

        public Location GetLocationbyId(int id)
        {
            using (masterEntities entities = new masterEntities())
            {
                return entities.Locations.Where(e => e.Id == id).FirstOrDefault();
            }
        }

        public Location SaveData(Location location)
        {            
            try
            { 
                using (masterEntities entities = new masterEntities())
                {
                    entities.Locations.Add(location);
                    entities.SaveChanges();                    
                    return location;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public Location UpdateData(int id, Location location)
        {
            try
            {
                using (masterEntities entities = new masterEntities())
                {
                    var entity = entities.Locations.Where(e => e.Id == id).FirstOrDefault();
                    if (entity == null)
                    {
                        return null;
                    }
                    else
                    {                     
                        entity.Country = location.Country;
                        entity.City = location.City;
                        entity.State = location.State;
                        entity.Title = location.Title;
                        entity.Zip = location.Zip;
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
