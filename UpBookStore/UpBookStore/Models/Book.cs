using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UpBookStore.Models
{
    public class Book
    {
        
        public int ID { get; set; }

        public string Name { get; set; }
        
        public int? Auftor_ID { get; set; }

        public int? Category_ID { get; set; }

        [ForeignKey("Auftor_ID")]
        public Auftor Auftor { get; set; }
        [ForeignKey("Category_ID")]
        public Categoryi Categoryi { get; set; }
        
    }
}