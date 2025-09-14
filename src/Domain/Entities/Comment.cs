using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.Constants;
using Domain.Events;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Comment : AggregateRoot, IAuditableEntity, ISoftDeletable
    {
        public int PostId { get; private set; }
        public int AuthorId { get; private set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; private set; } = string.Empty;

        // For threaded comments
        public int? ParentCommentId { get; private set; }
        public int Level { get; private set; } = 0;
        public int LikeCount { get; private set; } = 0;
        public int ReplyCount { get; private set; } = 0;

        // Audit properties
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Soft delete properties
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        // Navigation properties
        public Post Post { get; set; } = null!;
        public Profile Author { get; set; } = null!;
        public Comment? ParentComment { get; set; }
        public ICollection<Comment> Replies { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Report> Reports { get; set; } = new List<Report>();

        // Private constructor for EF Core
        private Comment() { }

        // Factory method for creating comments
        public static Comment Create(int postId, int authorId, string content, int? parentCommentId = null)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ContentValidationException("Comment content cannot be empty.");

            if (content.Length > BusinessRules.MaxCommentContentLength)
                throw new ContentValidationException($"Comment content cannot exceed {BusinessRules.MaxCommentContentLength} characters.");

            var comment = new Comment
            {
                PostId = postId,
                AuthorId = authorId,
                Content = content.Trim(),
                ParentCommentId = parentCommentId,
                Level = parentCommentId.HasValue ? 1 : 0, // Simple 2-level threading
                CreatedAt = DateTime.UtcNow
            };

            comment.AddDomainEvent(new CommentCreatedEvent(comment.Id, postId, authorId, content));
            return comment;
        }

        // Business methods
        public void UpdateContent(string newContent)
        {
            if (IsDeleted)
                throw new InvalidOperationException("Cannot update deleted comment.");

            if (string.IsNullOrWhiteSpace(newContent))
                throw new ContentValidationException("Comment content cannot be empty.");

            if (newContent.Length > BusinessRules.MaxCommentContentLength)
                throw new ContentValidationException($"Comment content cannot exceed {BusinessRules.MaxCommentContentLength} characters.");

            Content = newContent.Trim();
            UpdatedAt = DateTime.UtcNow;
        }

        public bool CanBeEditedBy(int userId)
        {
            return AuthorId == userId && !IsDeleted;
        }

        public void IncrementLikeCount()
        {
            LikeCount++;
        }

        public void DecrementLikeCount()
        {
            if (LikeCount > 0)
                LikeCount--;
        }

        public void IncrementReplyCount()
        {
            ReplyCount++;
        }

        public void DecrementReplyCount()
        {
            if (ReplyCount > 0)
                ReplyCount--;
        }

        public bool IsReply => ParentCommentId.HasValue;
        public bool CanHaveReplies => Level == 0; // Only top-level comments can have replies
    }
}