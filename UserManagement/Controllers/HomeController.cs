using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult validateuser(LoginModel fc)
        {
            var url = $"http://localhost:49378/api/login?email={fc.email}&pass={fc.pass}";
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var data = JsonConvert.DeserializeObject<Emp>(response.Content);
            if (data != null)
            {
                Session["empData"] = data;
                Session["UserName"] = data.FirstName + " " + data.LastName;
                if (data.UserRole == "Admin")
                {
                    return RedirectToAction("AdminDashBoard","Admin");
                }
                else if (data.UserRole == "User")
                {
                    return RedirectToAction("UserDashBoard","User");
                }
                else if (data.UserRole == "Account")
                {
                    return RedirectToAction("AccountDashBoard", "Account");
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            else
            {
                ModelState.AddModelError("Msg", "Invalid Login Credentials");
                return RedirectToAction("Login");
            }
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterForm(Emp emp)
        {
            string val = JsonConvert.SerializeObject(emp);
            var client = new RestClient("http://localhost:49378/api/employees");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", val, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (JsonConvert.DeserializeObject(response.Content).ToString() == "Data added successfully")
            {
                return Json(new { Success = response.StatusCode }, JsonRequestBehavior.AllowGet);
            }
            else { return Json(new { Success = "Failed" }, JsonRequestBehavior.AllowGet); }
        }
    }
}