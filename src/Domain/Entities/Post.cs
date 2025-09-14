using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.ValueObjects;
using Domain.Events;
using Domain.Constants;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Post : AggregateRoot, IAuditableEntity, ISoftDeletable
    {
        public int AuthorId { get; private set; }

        [Required]
        [StringLength(2000)]
        public PostContent Content { get; private set; } = new PostContent(string.Empty);

        public bool IsPublished { get; private set; } = true;
        public int ViewCount { get; private set; } = 0;
        public int LikeCount { get; private set; } = 0;
        public int CommentCount { get; private set; } = 0;
        public int ShareCount { get; private set; } = 0;

        // For reposted content
        public int? OriginalPostId { get; private set; }
        public bool IsRepost { get; private set; } = false;

        [StringLength(500)]
        public string? RepostComment { get; private set; }

        // Audit properties
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Soft delete properties
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        // Navigation properties
        public Profile Author { get; set; } = null!;
        public Post? OriginalPost { get; set; }
        public ICollection<Post> Reposts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Media> MediaFiles { get; set; } = new List<Media>();
        public ICollection<PostHashtag> PostHashtags { get; set; } = new List<PostHashtag>();
        public ICollection<Report> Reports { get; set; } = new List<Report>();

        // Private constructor for EF Core
        private Post() { }

        // Factory method for creating posts
        public static Post Create(int authorId, string content, bool isPublished = true)
        {
            var post = new Post
            {
                AuthorId = authorId,
                Content = new PostContent(content),
                IsPublished = isPublished,
                CreatedAt = DateTime.UtcNow
            };

            post.AddDomainEvent(new PostCreatedEvent(post.Id, authorId, content));
            return post;
        }

        // Factory method for creating reposts
        public static Post CreateRepost(int authorId, int originalPostId, string? repostComment = null)
        {
            var repost = new Post
            {
                AuthorId = authorId,
                Content = new PostContent(repostComment ?? ""),
                OriginalPostId = originalPostId,
                IsRepost = true,
                RepostComment = repostComment,
                CreatedAt = DateTime.UtcNow
            };

            return repost;
        }

        // Business methods
        public void UpdateContent(string newContent)
        {
            if (IsDeleted)
                throw new InvalidOperationException("Cannot update deleted post.");

            Content = new PostContent(newContent);
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddHashtag(Hashtag hashtag)
        {
            if (PostHashtags.Count >= BusinessRules.MaxHashtagsPerPost)
                throw new ContentValidationException($"Post cannot have more than {BusinessRules.MaxHashtagsPerPost} hashtags.");

            if (PostHashtags.Any(ph => ph.HashtagId == hashtag.Id))
                return; // Hashtag already exists

            PostHashtags.Add(new PostHashtag { PostId = Id, HashtagId = hashtag.Id });
        }

        public void RemoveHashtag(int hashtagId)
        {
            var postHashtag = PostHashtags.FirstOrDefault(ph => ph.HashtagId == hashtagId);
            if (postHashtag != null)
            {
                PostHashtags.Remove(postHashtag);
            }
        }

        public void AddMediaFile(Media mediaFile)
        {
            if (MediaFiles.Count >= BusinessRules.MaxMediaFilesPerPost)
                throw new ContentValidationException($"Post cannot have more than {BusinessRules.MaxMediaFilesPerPost} media files.");

            mediaFile.DisplayOrder = MediaFiles.Count + 1;
            MediaFiles.Add(mediaFile);
        }

        public void IncrementViewCount()
        {
            ViewCount++;
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

        public void IncrementCommentCount()
        {
            CommentCount++;
        }

        public void DecrementCommentCount()
        {
            if (CommentCount > 0)
                CommentCount--;
        }

        public void IncrementShareCount()
        {
            ShareCount++;
        }

        public void Publish()
        {
            if (IsDeleted)
                throw new InvalidOperationException("Cannot publish deleted post.");

            IsPublished = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Unpublish()
        {
            IsPublished = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public bool CanBeEditedBy(int userId)
        {
            return AuthorId == userId && !IsDeleted;
        }

        public IEnumerable<string> ExtractHashtags()
        {
            return Content.ExtractHashtags();
        }

        public IEnumerable<string> ExtractMentions()
        {
            return Content.ExtractMentions();
        }
    }
}