using MediatR;
using PArticle.Application.Abstractions.Interfaces;
using PArticle.Application.Models;
using PArticle.Application.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Features.Category.Commands.UpdateCategory
{
	public class UpdateCategoryCommandRequest:CategoryDto, IRequest<ResponseContainer<UpdateCategoryCommandResponse>>,IId
	{
		public int Id { get; set; }
	}
}
