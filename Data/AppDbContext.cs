using Microsoft.EntityFrameworkCore;
using TodoList.Model;

namespace TodoList.Data 
{
    public class AppDataContext : DbContext
    {
        public DbSet<TodoItem> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        => optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");
    }
}