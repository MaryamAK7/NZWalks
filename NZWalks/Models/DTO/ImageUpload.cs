﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO
{
    public class ImageUpload
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}
