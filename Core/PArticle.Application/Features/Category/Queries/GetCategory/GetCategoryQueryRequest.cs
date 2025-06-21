using MediatR;
using PArticle.Application.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Features.Category.Queries.GetCategory
{
	public class GetCategoryQueryRequest:GetByIdRequest<GetCategoryQueryResponse>
	{
		public GetCategoryQueryRequest(int id) : base(id) { }
	}
}
