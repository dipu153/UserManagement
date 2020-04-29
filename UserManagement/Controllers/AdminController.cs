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
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdminDashboard()
        {
            return View();
        }
        public ActionResult UpdateRole()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllUser()
        {
            var url = $"http://localhost:49378/api/Employees";
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var data = JsonConvert.DeserializeObject<List<Emp>>(response.Content);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateRoles(List<Emp> UpdateUsers)
        {
            string Msg = string.Empty;
            foreach (Emp ep in UpdateUsers)
            {
                string val = JsonConvert.SerializeObject(ep);
                var url = $"http://localhost:49378/api/Employees?email={ep.Email}&role={ep.UserRole}";
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.PUT);
                request.AddHeader("Content-Type", "application/json");
                IRestResponse response = client.Execute(request);
                if (JsonConvert.DeserializeObject(response.Content).ToString() == "Role assigned successfully")
                {
                    Msg = "Update Successful";
                }
                else
                {
                    Msg = "Failed to Update";
                    break;
                }
            }
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

    }
}