using Application.Authors.DataTransferObject;
using Application.Authors.Services;
using Domain.events;
using Domain.events.repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Handlers.Command
{
    public class CreateAuthorCommand : CreateAuthorRequest, IRequest<Result<AuthorResponse>>
    {

    }

    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Result<AuthorResponse>>
    {
        private readonly IAuthorServices _authorServices;
        public CreateAuthorCommandHandler(IAuthorServices authorServices)
        {
            _authorServices = authorServices;
        }
        public async Task<Result<AuthorResponse>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _authorServices.CreateAuthor(request);
                return new Result<AuthorResponse>
                {
                    IsSuccess = true,
                    Value = response,
                    ResponseCode = "200"
                };
            }
            catch (Exception ex)
            {
                return new Result<AuthorResponse>
                {
                    IsSuccess = false,
                    Value = null,
                    ResponseCode = "400"
                };
            }
        }
    }
}
