using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UpBookStore.Models
{
    public class Auftor
    {
        public int ID { get; set; }

        public string FIO { get; set; }

        public List<Book> Books { get; set; }

        public Auftor()
        {
            Books = new List<Book>();
        }

    }
}