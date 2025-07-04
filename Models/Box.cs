﻿using System.ComponentModel.DataAnnotations;

namespace PackSolverAPI.Models
{
    public class Box
    {
        public string BoxId { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }

        public ICollection<ProductBox>? productBoxes { get; set; }
    }
}
