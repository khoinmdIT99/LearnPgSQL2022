using Microsoft.AspNetCore.Mvc;
using PostgresqlEntityDemo.Data;
using PostgresqlEntityDemo.Models;
using System.Collections.Generic;

namespace PostgresqlEntityDemo.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public StudentsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Students> objStudentsList = _db.Students;
            return View(objStudentsList);
        }
        //get
        public IActionResult Create()
        {
            return View();
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(Students obj)
        {
            
            if (ModelState.IsValid) {

                _db.Students.Add(obj);

                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //get
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var studentFormDb = _db.Students.Find(id);
            if(studentFormDb == null)
            {
                return NotFound();
            }

            return View(studentFormDb);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Students obj)
        {

            if (ModelState.IsValid)
            {
                
                _db.Students.Update(obj);

                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //get//delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var studentFormDb = _db.Students.Find(id);
            if (studentFormDb == null)
            {
                return NotFound();
            }
            _db.Students.Remove(studentFormDb);
            _db.SaveChanges();
            return RedirectToAction("index");
        }
       
    }
}
