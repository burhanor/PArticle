using AutoMapper;
using PArticle.Application.Features.Tag.Commands.CreateTag;
using PArticle.Application.Features.Tag.Commands.UpdateTag;
using PArticle.Application.Features.Tag.Queries.GetTag;
using PArticle.Application.Features.Tag.Queries.GetTags;
using PArticle.Application.Models.Tag;

namespace PArticle.Application.Mappings
{
	internal class TagProfile:Profile
	{
		public TagProfile()
		{
			CreateMap<TagDto, CreateTagCommandRequest>();
			CreateMap<CreateTagCommandRequest, Domain.Entities.Tag>();
			CreateMap<Domain.Entities.Tag, CreateTagCommandResponse>();

			CreateMap<UpdateTagCommandRequest, Domain.Entities.Tag>();
			CreateMap<TagDto, UpdateTagCommandRequest>();
			CreateMap<Domain.Entities.Tag, UpdateTagCommandResponse>();


			CreateMap<Domain.Entities.Tag, GetTagQueryResponse>();

			CreateMap<Domain.Entities.Tag, GetTagsQueryResponse>();
			CreateMap<TagFilterModel, GetTagsQueryRequest>();
		}
	}
}
