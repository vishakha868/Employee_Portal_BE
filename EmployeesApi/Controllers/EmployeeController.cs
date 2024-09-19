using EmployeesApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace EmployeesApi.Controllers
{
    public class EmployeeController : ApiController
    {
        [EnableCorsAttribute("*", "*", "*")]
        public IEnumerable<EmployeeDb> Get()
        {
            using (EmployeeDbContext entity = new EmployeeDbContext())
            {
                return entity.EmployeeDbs.ToList();
            }
        }
        public EmployeeDb Get(int Id)
        {
            using (EmployeeDbContext entity = new EmployeeDbContext())
            {
                return entity.EmployeeDbs.FirstOrDefault(e => e.Id == Id);
            }
        }

        public string Post([FromBody] EmployeeDb values)
        {
            Console.Write(values);
                using (EmployeeDbContext entity = new EmployeeDbContext())
                {
                    var checkId = entity.EmployeeDbs.FirstOrDefault(e => e.Id == values.Id);
                    if (checkId == null)
                    {
                        entity.EmployeeDbs.Add(values);
                        entity.SaveChanges();
                        return ("Successfully Created a new Employee");
                    }
                    else
                    {
                        return ("user already existed");
                    }
            }
        }
        public string Delete(int id)
        {
            using(EmployeeDbContext entity = new EmployeeDbContext())
            {
                var checkId = entity.EmployeeDbs.FirstOrDefault(e => e.Id ==id);
                if (checkId!= null)
                {
                    entity.EmployeeDbs.Remove(entity.EmployeeDbs.FirstOrDefault(e => e.Id == id));
                    entity.SaveChanges();
                    return ("Successfully Deleted");
                }
                else
                    return ("Employee Does not Exist");
            }
        }

        public string Put(int id, [FromBody] EmployeeDb employee)
        {
            using(EmployeeDbContext entity=new EmployeeDbContext())
            {
                var emp=entity.EmployeeDbs.FirstOrDefault(e=>e.Id==id);
                emp.Name=employee.Name;
                emp.Email=employee.Email;
                emp.JoiningDate = employee.JoiningDate;
                emp.Salary = employee.Salary;
                emp.Department = employee.Department;
                emp.Phone = employee.Phone;
                entity.SaveChanges();
                return ("Successfully Updated the Employee");
            }
        }
    }
}