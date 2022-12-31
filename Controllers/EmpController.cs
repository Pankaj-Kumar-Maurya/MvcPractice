using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPractice.Models;

namespace MvcPractice.Controllers
{
    public class EmpController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        public ActionResult Add(int id=0)
        {
            ViewBag.vb = "Save";
            Student obj = new Student();
            if(id>0)
            {
                var data = db.Students.Where(x => x.Id == id).ToList();
                obj.Id = data[0].Id;
                obj.Name= data[0].Name;
                obj.Age = data[0].Age;
                ViewBag.vb = "Update";
            }
            return View(obj);
        }
        [HttpPost]
        public ActionResult Add(Student student)
        {
            if(student.Id>0)
            {
                db.Entry(student).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {

            db.Students.Add(student);
            }
            db.SaveChanges();
            return RedirectToAction("Show");
        }
        public ActionResult Show()
        {
            return View(db.Students.ToList());
        }
        public ActionResult Delete(int id=0)
        {
            var data = db.Students.Find(id);
            db.Students.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Show");
        }
    }
}