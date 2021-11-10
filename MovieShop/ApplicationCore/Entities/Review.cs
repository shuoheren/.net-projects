﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Review
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public decimal Rating { get; set; }
        [MaxLength(int.MaxValue)]
        public string? ReviewText { get; set; }
        public Movie Movie { get; set; }
        public User User { get; set; }
    }
}