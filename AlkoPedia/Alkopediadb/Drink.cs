using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace AlkoPedia.Alkopediadb
{
    class Drink
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Ingredients { get; set; }
        public string Cooking { get; set; }
        public int Lvl { get; set; }
        public string User { get; set; }
    }
    class DrinkContext : DbContext
    {
        public DrinkContext() : base("Alkopediadb") { }
        public DbSet<Drink> Drinks { get; set; }
    }
}
