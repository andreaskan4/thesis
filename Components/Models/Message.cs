using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Thesis.Components.Models;

namespace Thesis.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        public string SenderName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string SenderEmail { get; set; } = string.Empty;

        [Required]
        public string SenderRole { get; set; } = string.Empty; // "Student" or "Teacher"

        [Required]
        public string Subject { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public bool IsRead { get; set; } = false;
        public DateTime? ReadAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int RecipientId { get; set; }
        public string? RecipientName { get; set; }
        public string? RecipientEmail { get; set; }
        public string? RecipientRole { get; set; }
        public int SenderId { get; set; }

        // Navigation property for replies
        public List<MessageReply> Replies { get; set; } = new List<MessageReply>();
    }
}