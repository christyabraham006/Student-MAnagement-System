using Student_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Student_Management_System.Controllers
{
    public class StudentController : Controller
    {
        StudentDBHandler sbd = new StudentDBHandler();
        // GET: Student
        public ActionResult Index()
        {
            try
            {
                List<StudentModel> students = sbd.GetStudents();

                return View(students);
            }
             catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(StudentModel student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (sbd.AddNewStudent(student))
                    {
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed");
                        return View();
                    } 
                }
                return View(student);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message + " " + ex.StackTrace);
                return View(student);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
               
                if (sbd.DeleteStudent(id))
                {
                    ViewBag.AlertMsg = "Student Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int Id)
        {
            StudentModel student = sbd.GetById(Id);
            return View(student);
        }
        [HttpPost]
        public ActionResult Edit(StudentModel student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (sbd.UpdateDetails(student))
                    {
                        ModelState.Clear();
                        //ModelState.AddModelError("","Sucessfully added");
                        return RedirectToAction("Index");
                    }
                }
                return View(student);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message + " " + ex.StackTrace);
                return View(student);
            }
        }







    }
}