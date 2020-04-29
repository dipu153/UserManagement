using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    public class EmployeesController : ApiController
    {
        public IEnumerable<Employee> Get()
        {
            using (EmployeeDBContext dbContext = new EmployeeDBContext())
            {
                return dbContext.Employees.ToList();
            }
        }
        public Employee Get(string email)
        {
            using (EmployeeDBContext dbContext = new EmployeeDBContext())
            {
                return dbContext.Employees.FirstOrDefault(e => e.Email == email);
            }
        }
        public string post(Employee emp)
        {
            using (EmployeeDBContext dbContext = new EmployeeDBContext())
            {
                var message = string.Empty;
                try
                {
                    emp.VisitLink = false;
                    emp.Reminder = 0;
                    var data = dbContext.Employees.Add(emp);
                    dbContext.SaveChanges();
                    if (data != null)
                    {
                        message = "Data added successfully";
                    }
                }
                catch (Exception ex)
                {
                    message = "Error while adding data. Error: " + ex.Message;
                }
                return message;
            }
        }
        public string put(string email, string role)
        {
            using (EmployeeDBContext dbContext = new EmployeeDBContext())
            {
                var message = string.Empty;
                try
                {
                    var emp = dbContext.Employees.Where(e => e.Email == email).FirstOrDefault();
                    if (emp != null)
                    {
                        emp.UserRole = role;
                        dbContext.SaveChanges();
                        message = "Role assigned successfully";
                    }
                }
                catch (Exception ex)
                {
                    message = "Error while adding data. Error: " + ex.Message;
                }
                return message;
            }
        }
        public string get(string email, bool visit)
        {
            using (EmployeeDBContext dbContext = new EmployeeDBContext())
            {
                var message = string.Empty;
                try
                {
                    var emp = dbContext.Employees.Where(e => e.Email == email).FirstOrDefault();
                    if (emp != null)
                    {
                        emp.VisitLink = visit;
                        dbContext.SaveChanges();
                        string name = emp.FirstName + " " + emp.LastName;
                        MailMessage mm = new MailMessage("dash.chandrakant153@gmail.com", email);
                        mm.Subject = "Thank you " + name + "";
                        mm.Body = "<b>Thank you for activation</b>";
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential();
                        credentials.UserName = "<FromMail>";
                        credentials.Password = "<Password>";
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = credentials;
                        smtp.Port = 587;
                        smtp.Send(mm);
                        message = "Thank you";
                    }
                }
                catch (Exception ex)
                {
                    message = "Error while adding data. Error: " + ex.Message;
                }
                return message;
            }
        }

        public string put(string email)
        {
            using (EmployeeDBContext dbContext = new EmployeeDBContext())
            {
                var message = string.Empty;
                try
                {
                    var emp = dbContext.Employees.Where(e => e.Email == email).FirstOrDefault();
                    if (emp != null)
                    {
                        emp.Reminder = emp.Reminder + 1;
                        dbContext.SaveChanges();
                        message = "R";
                    }
                }
                catch (Exception ex)
                {
                    message = "Error while adding data. Error: " + ex.Message;
                }
                return message;
            }
        }
    }
}
