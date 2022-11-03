using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAPI.Modes
{
    public class User
    {
        [Key]
        [ForeignKey("Employee")]
        public int Id { get; set; }
        public string Password { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        [JsonIgnore]
        public Role? Role { get; set; }
        [JsonIgnore]
        public Employee? Employee { get; set; }
    }
}
