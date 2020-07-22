using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace UpBookStore.Models
{
    public class BookStoreContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Auftor> Auftors { get; set; }

        public DbSet<Categoryi> Categoryis { get; set; }

        public DbSet<Sell> Sells { get; set; }

        
    }

    
}