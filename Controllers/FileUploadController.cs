using CoreMVC.Models;
using CoreMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Xml;

namespace CoreMVC.Controllers
{
    public class FileUploadController : Controller       //nahrani xml souboru do programu
    { StudentsService studentsService;

        public FileUploadController(StudentsService studentsService)
        {
            this.studentsService = studentsService;
        }
        [Authorize(Roles="Admin")]
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)   //parametr file musi sedet s student/index =>input name
        {
            string filePath = "";
            if (file.Length > 0)
            {
                filePath = Path.GetFullPath(file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);   //nakopiruje xml
                    stream.Close();                     //nakopiruje xml
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(filePath);
                    XmlElement koren = xmlDoc.DocumentElement;
                    foreach (XmlNode node in koren.SelectNodes("/Students/Student"))
                    {
                        Student s = new Student    //s tim ze vime jak je xml napsane a dle toho zpracovavame
                        {
                            FirstName = node.ChildNodes[0].InnerText,
                            LastName = node.ChildNodes[1].InnerText,
                            DateOfBirth = DateTime.Parse(node.ChildNodes[2].InnerText, CultureInfo.CreateSpecificCulture("cs-CZ"))   //musi se specifikovat dle zeme,kde se zpracovava
                        };
                        await studentsService.CreateAsync(s);
                    }
                }
                return RedirectToAction("Index", "Students");
            }
            else return View("NotFound");

        }

    }
}
