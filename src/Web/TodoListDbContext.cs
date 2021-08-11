using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Entities;

namespace Web
{
    public class TodoListDbContext : DbContext   // Creating our DBContext class which inherits DbContext (built in)
    {
        public DbSet<Todo> Todos { get; set; }

        public TodoListDbContext(DbContextOptions<TodoListDbContext> options): base(options)
        {

        }
    }
}
