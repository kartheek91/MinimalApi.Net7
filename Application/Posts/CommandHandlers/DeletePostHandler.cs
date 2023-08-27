using Application.Abstractions;
using Application.Posts.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.CommandHandlers
{
    public class DeletePostHandler : IRequestHandler<DeletePost>
    {
        private readonly IPostRepository _postRepository;
        public DeletePostHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<Unit> Handle(DeletePost delete, CancellationToken cancellationToken) 
        {
            await _postRepository.DeletePost(delete.PostId);
            return Unit.Value;
        }
    }
}
