using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models.WebAPI.Modes;

namespace WebAPI.ViewModel
{
    public class EmployeeUserRoleClient
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public DateTime BirthDate { get; set; }

        public string Gender { get; set; }

        public int Salary { get; set; }
        public string ClientName { get; set; }
        public string City { get; set; }
    }
}
