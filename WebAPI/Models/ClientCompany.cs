namespace WebAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    namespace WebAPI.Modes
    {
        public class ClientCompany
        {
            [Key]
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }

}