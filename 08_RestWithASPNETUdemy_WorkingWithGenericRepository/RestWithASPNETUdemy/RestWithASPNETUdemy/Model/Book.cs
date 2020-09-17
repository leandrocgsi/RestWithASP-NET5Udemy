using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNETUdemy.Model
{
    [Table("books")]
    public class Book
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("author")]
        public string Author { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("launch_date")]
        public DateTime LaunchDate { get; set; }
    }

}
