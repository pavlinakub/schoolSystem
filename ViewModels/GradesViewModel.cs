using CoreMVC.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoreMVC.ViewModels
{
    public class GradesViewModel
    {
        public int Id { get; set; }
        [DisplayName("Student Name")]
        public int StudentId { get; set; }
        [DisplayName("Subject")]
        public int SubjectId { get; set; }
        [DisplayName("What")]
        public string What { get; set; }
        [DisplayName("Grade")]
        public int Mark { get; set; }
        public DateTime Date { get; set; }
    }
}
