using Application.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialDbContext _ctx;
        public PostRepository(SocialDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Post> CreatePost(Post post)
        {
            post.DateCreated = DateTime.UtcNow;
            post.LastModified = DateTime.UtcNow;
            _ctx.Add(post);
            await _ctx.SaveChangesAsync();
            return post;
        }

        public async Task DeletePost(int postId)
        {
            var post = await _ctx.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if (post == null) return;
            _ctx.Remove(post);
            await _ctx.SaveChangesAsync();
        }

        public async Task<ICollection<Post>> GetAllPosts()
        {
            return await _ctx.Posts.ToListAsync();
        }

        public async Task<Post> GetPostsById(int postId)
        {
            return await _ctx.Posts.FirstOrDefaultAsync(p => p.Id == postId);
        }

        public async Task<Post> UpdatePost(string updatedContent, int postId)
        {
            var post = await _ctx.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            post.LastModified = DateTime.UtcNow;
            post.Content = updatedContent;
            await _ctx.SaveChangesAsync();
            return post;
        }
    }
}
