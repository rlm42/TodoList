using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entities
{
    [Table(nameof(Todo))]
    public class Todo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDone { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
