﻿using Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}-\d{10}$", ErrorMessage = "Invalid ISBN format. Correct format is xxx-xxxxxxxxxx.")]
        public string ISBN { get; set; }

        public DateTime PublishedDate { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }

}
