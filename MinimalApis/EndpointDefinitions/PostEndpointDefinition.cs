using Application.Posts.Commands;
using Application.Posts.Queries;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Hosting;
using MinimalApis.Abstractions;
using MinimalApis.Filters;

namespace MinimalApis.EndpointDefinitions
{
    public class PostEndpointDefinition : IEndpointDefinition 
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var posts = app.MapGroup("api/posts");
            
            posts.MapGet("/{id}", GetPostById)
                .WithName("GetPostsById");

            posts.MapGet("/", GetAllPosts);

            posts.MapPost("/", CreatePost).AddEndpointFilter<PostValidationFilter>();

            posts.MapPut("/{id}", UpdatePost);

            posts.MapDelete("/{id}", DeletePostById);

        }

        private async Task<IResult> GetPostById(IMediator mediator, int id)
        {
            var getPost = new GetPostById { PostId = id };
            var post = await mediator.Send(getPost);
            return TypedResults.Ok(post);
        }

        private async Task<IResult> GetAllPosts(IMediator mediator)
        {
            var getCommand = new GetAllPosts { };
            var posts = await mediator.Send(getCommand);
            return TypedResults.Ok(posts);
        }
        private async Task<IResult> CreatePost(IMediator mediator, Post post)
        {
            var createPost = new CreatePost { Content = post.Content };
            var createdPost = await mediator.Send(createPost);
            return Results.CreatedAtRoute("GetPostsById", new { createdPost.Id }, createdPost);
        }

        private async Task<IResult> UpdatePost(IMediator mediator, Post post, int id)
        {
            var updatePost = new UpdatePost { PostId = id, Content = post.Content };
            var updatedPost = await mediator.Send(updatePost);
            return TypedResults.Ok(updatedPost);
        }

        private async Task<IResult> DeletePostById(IMediator mediator, int id)
        {
            var deletePost = new DeletePost { PostId = id };
            await mediator.Send(deletePost);
            return TypedResults.NoContent();
        }
    }
}
