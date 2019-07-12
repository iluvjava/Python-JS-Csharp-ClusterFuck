using System;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Models
{
   
    public class MyLittleEntity
    {
        [Key]
        public int ID { get; set; } // The primary key
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [DataType(DataType.Text)]
        public string Content { get; set; }
    }
    
}
