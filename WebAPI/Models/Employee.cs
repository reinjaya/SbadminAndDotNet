using System.ComponentModel.DataAnnotations;

namespace WebAPI.Modes
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public string Gender { get; set; }

        public int Salary { get; set; }
        public string City { get; set; }
    }
}
