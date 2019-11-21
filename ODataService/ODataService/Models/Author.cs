﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("Author")]
    public class Author
    {
        public Author()
        {
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
