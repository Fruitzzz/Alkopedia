using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace AlkoPedia.Alkopediadb
{
    class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Drinks { get; set; }
    }
    class UserContext : DbContext
    {
        public UserContext() : base("Alkopediadb") { }
        public DbSet<User> Users { get; set; }
    }
}
