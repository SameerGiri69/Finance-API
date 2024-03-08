﻿using System.ComponentModel.DataAnnotations;

namespace Finshark_API.DTOs.Comment
{
    public class UpdateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
        [MaxLength(280, ErrorMessage = "Title cannot exceed 280 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be at least 5 characters")]
        [MaxLength(280, ErrorMessage = "Content cannot exceed 280 characters")]
        public string Content { get; set; } = string.Empty;

    }
}
