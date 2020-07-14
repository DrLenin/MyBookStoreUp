using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UpBookStore.Models
{
    public class Sell
    {
        public int ID { get; set; }

        public int? ID_Books { get; set; }

        public int Price { get; set; }

        public string Email { get; set; }

        [ForeignKey("ID_Books")]
        public Book Book { get; set; }
    }
}