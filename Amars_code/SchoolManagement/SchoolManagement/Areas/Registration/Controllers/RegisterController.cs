using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.Areas.Registration.Controllers
{
    public class RegisterController : Controller
    {
        Registerdblayer register = new Registerdblayer();
        // GET: Registration/Register
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ActionName("Student_ContactPerson_Info")]
        public ActionResult Student_ContactPerson_InfoPost()
        {
            if (ModelState.IsValid)
            {
                Models.StudentRegisterInformation st = new Models.StudentRegisterInformation();
                UpdateModel(st);
                register.AddStudentInfo(st);
            }
            return View();
        }

        [HttpGet]
        [ActionName("Student_ContactPerson_Info")]
        public ActionResult Student_ContactPerson_InfoGet()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Instructor_Info")]
        public ActionResult Instructor_InfoPost()
        {
            if (ModelState.IsValid)
            {
                Instructor it = new Instructor();
                UpdateModel(it);
                register.AddInstructorInfo(it);
                
            }
            return View();
        }
        [HttpGet]
        [ActionName("Instructor_Info")]
        public ActionResult Instructor_InfoGet()
        {
            return View();
        }
        //        // GET: Registration/Register/Details/5
        //        public ActionResult Details(int id)
        //        {
        //            return View();
        //        }

        //        // GET: Registration/Register/Create
        //        public ActionResult Create()
        //        {
        //            return View();
        //        }

        //        // POST: Registration/Register/Create
        //        [HttpPost]
        //        public ActionResult Create(FormCollection collection)
        //        {
        //            try
        //            {
        //                // TODO: Add insert logic here

        //                return RedirectToAction("Index");
        //            }
        //            catch
        //            {
        //                return View();
        //            }
        //        }

        //        // GET: Registration/Register/Edit/5
        //        public ActionResult Edit(int id)
        //        {
        //            return View();
        //        }

        //        // POST: Registration/Register/Edit/5
        //        [HttpPost]
        //        public ActionResult Edit(int id, FormCollection collection)
        //        {
        //            try
        //            {
        //                // TODO: Add update logic here

        //                return RedirectToAction("Index");
        //            }
        //            catch
        //            {
        //                return View();
        //            }
        //        }

        //        // GET: Registration/Register/Delete/5
        //        public ActionResult Delete(int id)
        //        {
        //            return View();
        //        }

        //        // POST: Registration/Register/Delete/5
        //        [HttpPost]
        //        public ActionResult Delete(int id, FormCollection collection)
        //        {
        //            try
        //            {
        //                // TODO: Add delete logic here

        //                return RedirectToAction("Index");
        //            }
        //            catch
        //            {
        //                return View();
        //            }
        //        }
    }
}
