using System.ComponentModel.DataAnnotations;

namespace CoreMVC.Models
{
    public class Subject
    {public int Id { get; set; }
        [StringLength(25)]      //omezeni delky stringu max 25 znaku (vaze se k Name)
        public string Name { get; set; }    
    }
}
