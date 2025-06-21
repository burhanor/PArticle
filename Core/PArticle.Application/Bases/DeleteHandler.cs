using AutoMapper;
using Domain.Contracts.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Enums;
using PArticle.Application.Interfaces;
using PArticle.Application.Models;

namespace PArticle.Application.Bases
{
	public class DeleteHandler<T, TRequest>(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, string successMessage,string failMessage) : BaseHandler<T>(uow, httpContextAccessor, mapper), IRequestHandler<TRequest, ResponseContainer<Unit>>
		where T : class, IEntityBase, new()
		where TRequest : class, IDeleteRequest, new()
	{
		public async virtual Task<ResponseContainer<Unit>> Handle(TRequest request, CancellationToken cancellationToken)
		{
			ResponseContainer<Unit> response = new();
			try
			{
				if (request.Ids?.Count > 0)
				{
					writeRepository.Delete(request.Ids);
					await uow.SaveChangesAsync(cancellationToken);
					response.Status = ResponseStatus.Success;
					response.Message = successMessage;
				}

			}
			catch (Exception ex)
			{
				response.Status = ResponseStatus.Failed;
				response.Message = failMessage;
			}

			return response;
		}

		


	}
}
