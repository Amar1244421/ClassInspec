using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.Areas.TestCenter.Controllers
{
    public class TestCenterController : Controller
    {
        // GET: TestCenter/TestCenter
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DisplayTests(string text)
        {
            TestCenterDbLayer businesslayer = new TestCenterDbLayer();
            SchoolManagement.Areas.TestCenter.Models.TestCenter std = new SchoolManagement.Areas.TestCenter.Models.TestCenter();
            std.ClassID = Int32.Parse(text);
            DataSet dt = businesslayer.GetTestInfo(std);

            var plist = dt.Tables[0].AsEnumerable().Select(x => new SchoolManagement.Areas.TestCenter.Models.TestCenter
            {
                TestID = x.Field<int>("TestID"),
                ClassID = x.Field<int>("ClassID"),
                TestName = x.Field<string>("TestName"),
                outOFF = x.Field<int>("outOFF"),
                Description = x.Field<string>("Description"),

            });
            return View(plist);
        }
        public ActionResult GetTestDetails()
        {
            return View();
        }

       

        [HttpPost]
        [ActionName("CreateTest")]
        public ActionResult CreateTestPost()
        {
            if (ModelState.IsValid)
            {
                Models.TestCenter st = new Models.TestCenter();
                UpdateModel(st);
                TestCenterDbLayer register = new TestCenterDbLayer();
                register.AddTestInfo(st);
            }
            return View();
        }

        [HttpGet]
        [ActionName("CreateTest")]
        public ActionResult CreateTest()
        {
            return View();
        }

    }
}