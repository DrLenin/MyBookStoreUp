using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UpBookStore.Models
{
    public class Categoryi
    {
        public int ID { get; set; }

        public string Names { get; set; }

        public List<Book> Books { get; set; }

        public Categoryi()
        {
            Books = new List<Book>();
        }

    }
}