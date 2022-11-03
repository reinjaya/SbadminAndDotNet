using System.ComponentModel.DataAnnotations;

namespace WebAPI.Modes
{
    public class Division
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }    
    }
}
