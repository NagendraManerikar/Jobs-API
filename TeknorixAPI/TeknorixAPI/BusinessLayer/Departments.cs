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
    public interface IDepartment
    {
        IEnumerable<Department> GetDepartments();
        Department GetDepartmentbyId(int Id);
        Department SaveData(Department department);
        Department UpdateData(int id, Department department);
    }
    public class DeparmentResults : IDepartment
    {
        public IEnumerable<Department> GetDepartments()
        {
            using (masterEntities entities = new masterEntities())
            {
                var departments = entities.Departments.ToList();
                return departments;
            }
        }

        public Department GetDepartmentbyId(int id)
        {
            using (masterEntities entities = new masterEntities())
            {
                return entities.Departments.Where(e => e.Id == id).FirstOrDefault();
            }
        }

        public Department SaveData(Department department)
        {
            try
            {
                using (masterEntities entities = new masterEntities())
                {
                    entities.Departments.Add(department);
                    entities.SaveChanges();
                    return department;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Department UpdateData(int id, Department department)
        {
            try
            {
                using (masterEntities entities = new masterEntities())
                {
                    var entity = entities.Departments.Where(e => e.Id == id).FirstOrDefault();
                    if (entity == null)
                    {
                        return null;
                    }
                    else
                    {
                        entity.Name = department.Name;
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