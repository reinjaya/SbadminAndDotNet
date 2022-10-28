using MessagePack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Departement
    {
        public Departement(int Id, string Name, int DivisionId)
        {
            this.Id = Id;
            this.Name = Name;
            this.DivisionId = DivisionId;
        }

        public Departement()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int DivisionId { get; set; }
        public Division Division { get; set; }
    }
}
