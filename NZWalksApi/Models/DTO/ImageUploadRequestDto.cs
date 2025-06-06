﻿using System.ComponentModel.DataAnnotations;

namespace NZWalksApi.Models.DTO
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string? FileDescription { get; set; }
    }
}
