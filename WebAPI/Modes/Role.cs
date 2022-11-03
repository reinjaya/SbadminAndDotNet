using System.ComponentModel.DataAnnotations;

namespace WebAPI.Modes
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
