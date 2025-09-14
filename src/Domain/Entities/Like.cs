using Domain.Common;
using Domain.Events;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Like : AggregateRoot
    {
        public int ProfileId { get; private set; }

        // Either PostId or CommentId will be set, not both
        public int? PostId { get; private set; }
        public int? CommentId { get; private set; }

        // Navigation properties
        public Profile Profile { get; set; } = null!;
        public Post? Post { get; set; }
        public Comment? Comment { get; set; }

        // Private constructor for EF Core
        private Like() { }

        // Factory method for creating post likes
        public static Like CreatePostLike(int profileId, int postId)
        {
            var like = new Like
            {
                ProfileId = profileId,
                PostId = postId,
                CommentId = null,
                CreatedAt = DateTime.UtcNow
            };

            like.AddDomainEvent(new PostLikedEvent(postId, profileId, 0)); // AuthorId will be set by the application layer
            return like;
        }

        // Factory method for creating comment likes
        public static Like CreateCommentLike(int profileId, int commentId)
        {
            var like = new Like
            {
                ProfileId = profileId,
                PostId = null,
                CommentId = commentId,
                CreatedAt = DateTime.UtcNow
            };

            like.AddDomainEvent(new CommentLikedEvent(commentId, profileId));
            return like;
        }

        // Business methods
        public void Unlike()
        {
            if (PostId.HasValue)
            {
                AddDomainEvent(new PostUnlikedEvent(PostId.Value, ProfileId, 0)); // AuthorId will be set by the application layer
            }
            else if (CommentId.HasValue)
            {
                AddDomainEvent(new CommentUnlikedEvent(CommentId.Value, ProfileId));
            }
        }

        // Query methods
        public bool IsPostLike => PostId.HasValue;
        public bool IsCommentLike => CommentId.HasValue;

        // Validation method
        public static void ValidateUniqueLike(int profileId, int? postId, int? commentId, IEnumerable<Like> existingLikes)
        {
            var duplicate = existingLikes.FirstOrDefault(l =>
                l.ProfileId == profileId &&
                l.PostId == postId &&
                l.CommentId == commentId);

            if (duplicate != null)
                throw new ContentValidationException("User has already liked this content.");
        }
    }
}