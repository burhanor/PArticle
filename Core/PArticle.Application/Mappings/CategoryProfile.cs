using AutoMapper;
using PArticle.Application.Features.Category.Commands.CreateCategory;
using PArticle.Application.Features.Category.Commands.UpdateCategory;
using PArticle.Application.Features.Category.Queries.GetCategories;
using PArticle.Application.Features.Category.Queries.GetCategory;
using PArticle.Application.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Mappings
{
	internal class CategoryProfile : Profile
	{
		public CategoryProfile()
		{
			CreateMap<CategoryDto, CreateCategoryCommandRequest>();
			CreateMap<CreateCategoryCommandRequest, Domain.Entities.Category>();
			CreateMap<Domain.Entities.Category, CreateCategoryCommandResponse>();

			CreateMap<UpdateCategoryCommandRequest, Domain.Entities.Category>();
			CreateMap<CategoryDto, UpdateCategoryCommandRequest>();
			CreateMap<Domain.Entities.Category, UpdateCategoryCommandResponse>();


			CreateMap<Domain.Entities.Category, GetCategoryQueryResponse>();

			CreateMap<Domain.Entities.Category, GetCategoriesQueryResponse>();
			CreateMap<CategoryFilterModel,GetCategoriesQueryRequest>();
		}
	}
}
