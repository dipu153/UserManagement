using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    public class LoginController : ApiController
    {
        public Employee Get(string email, string pass)
        {
            var emp = new Employee();
            try
            {
                using (EmployeeDBContext dbContext = new EmployeeDBContext())
                {
                    emp = dbContext.Employees.FirstOrDefault(e => e.Email == email && e.Password == pass);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            return emp;
        }
    }
}
