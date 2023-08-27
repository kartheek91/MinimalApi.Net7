using Application.Abstractions;
using Application.Posts.Commands;
using Application.Posts.Queries;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.QueryHandlers
{
    public class GetAllPostsHandler : IRequestHandler<GetAllPosts, ICollection<Post>>
    {
        private readonly IPostRepository _postRepository;

        public GetAllPostsHandler(IPostRepository postRepository) 
        {
            _postRepository = postRepository;
        }
        public async Task<ICollection<Post>> Handle(GetAllPosts getAllPosts, CancellationToken cancellationToken)
        {
            return await _postRepository.GetAllPosts();
        }
    }
}
