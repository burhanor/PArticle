using AutoMapper;
using PArticle.Application.Abstractions.Interfaces.Token;
using PArticle.Application.Features.Auth.Commands.Login;
using PArticle.Application.Features.Auth.Commands.Register;
using PArticle.Application.Models.Auth;

namespace PArticle.Application.Mappings
{
	internal class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<RegisterCommandRequest, Domain.Entities.User>();
			CreateMap<RegisterModel, RegisterCommandRequest>();
			CreateMap<Domain.Entities.User, RegisterCommandResponse>();
			CreateMap<Domain.Entities.User, UserDto>();


			CreateMap<Domain.Entities.User, LoginCommandResponse>();
		}
	}
}
