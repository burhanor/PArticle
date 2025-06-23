using MediatR;
using PArticle.Application.Models;
using PArticle.Application.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Features.Category.Commands.CreateCategory
{
	public class CreateCategoryCommandRequest: CategoryDto, IRequest<ResponseContainer<CreateCategoryCommandResponse>>
	{
	}
}
