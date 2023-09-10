using CoreMVC.Services;
using CoreMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreMVC.Controllers
{
    [Authorize]   //viditelny az po prihlaseni 
    public class GradesController : Controller
    {
        GradeService service;
        public GradesController(GradeService service)
        {
            this.service = service;
            
        }
        public async Task<IActionResult> Index()     //asynchroni metoda by mela vratit Task<>
        {   var allGrades=await service.GetGradesAsync();
            return View(allGrades);
        }
       //[Authorize(Roles ="Teacher,Admin")]   //pristup jen pro ucitele
        public async Task<IActionResult> Create()
        {
            var gradesDropDownsData = await service.GetGradesDropDownValues();
            ViewBag.Students = new SelectList(gradesDropDownsData.Students, "Id", "LastName");
            ViewBag.Subjects = new SelectList(gradesDropDownsData.Subjects, "Id", "Name");
            return View();
        }
        [Authorize(Roles = "Teacher,Director,Admin,Visitor")]
        [HttpPost]
        public async Task<IActionResult> Create(GradesViewModel newGrade)
        {
            await service.CreateAsync(newGrade);
            return RedirectToAction("Index");
        }
       // [Authorize(Roles = "Teacher,Admin,Director")]   //pristup jen pro ucitele
        public async Task<IActionResult> Edit(int id)
        {
            var gradeToEdit = await service.GetByIdAsync(id);
            if(gradeToEdit == null)
            {
                return View("NotFound");
            }
            var gradesDropDownsData = await service.GetGradesDropDownValues();
            ViewBag.Students = new SelectList(gradesDropDownsData.Students, "Id", "LastName");
            ViewBag.Subjects = new SelectList(gradesDropDownsData.Subjects, "Id", "Name");
            var gradeVMtiEdit = new GradesViewModel()
            {
                Id = gradeToEdit.Id,
                Date = gradeToEdit.Date,
                Mark = gradeToEdit.Mark,
                StudentId = gradeToEdit.Student.Id,
                SubjectId = gradeToEdit.Subject.Id,
                What = gradeToEdit.What
            };
            return View();
        }
        [Authorize(Roles = "Teacher,Director,Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id,GradesViewModel updatedGrade)
        {
            await service.UpdateAsync(id, updatedGrade);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Teacher,Admin,Director")]   //pristup jen pro ucitele
        public async Task<IActionResult>DeleteAsync(int id)
        {
            await service.DeleteAsync(id);
            return RedirectToAction("Index");
        } 
    }
}
