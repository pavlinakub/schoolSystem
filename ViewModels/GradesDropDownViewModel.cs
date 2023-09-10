using CoreMVC.Models;

namespace CoreMVC.ViewModels
{
    public class GradesDropDownViewModel
    {
        public List<Student> Students { get; set; } 
        public List<Subject> Subjects { get; set; }
        public GradesDropDownViewModel()
        {
            Students = new List<Student>();
            Subjects = new List<Subject>();
        }
    }
}
