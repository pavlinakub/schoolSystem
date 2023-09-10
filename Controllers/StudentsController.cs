using CoreMVC.Models;
using CoreMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVC.Controllers
{
    [Authorize(Roles = "Admin,Teacher,Director,Visitor")]
    public class StudentsController : Controller
    {
        public StudentsService _service;
        public StudentsController(StudentsService service)
        {
            this._service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allStudents = await _service.GetAllAsync();
            return View(allStudents);
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _service.GetByIdAsync(id);
            return View("Details", student);
        }
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Teacher,Director")]
        [HttpPost]
        public async Task<IActionResult> Create(Student newStudent)
        {
            await _service.CreateAsync(newStudent);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var studentToEdit = await _service.GetByIdAsync(id);
            if (studentToEdit == null)
            {
                return View("NotFound");
            }
            return View(studentToEdit);
        }
        [Authorize(Roles = "Admin,Teacher,Director")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, FirstName, LastName, DateOfBirth")] Student updatedStudent)
        {
            await _service.UpdateAsync(id, updatedStudent);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin,Teacher,Director")]
        public async Task<IActionResult> Delete(int id)
        {
            var studentToDelete = await _service.GetByIdAsync(id);
            if (studentToDelete == null)
            {
                return View("NotFound");
            }
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
