namespace CoreMVC.Models
{
    // [PrimaryKey("Id")] - tim zrusim atomaticky primary key ktery se udela z id
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }   //string? udela pri migraci nullable true-->nemusi byt zadan 
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}