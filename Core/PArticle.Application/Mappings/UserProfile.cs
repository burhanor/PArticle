using AutoMapper;
using PArticle.Application.Abstractions.Interfaces.Token;
using PArticle.Application.Features.Auth.Commands.Login;
using PArticle.Application.Features.Auth.Commands.Register;
using PArticle.Application.Features.User.Commands.CreateUser;
using PArticle.Application.Features.User.Commands.UpdateUser;
using PArticle.Application.Features.User.Queries.GetUser;
using PArticle.Application.Features.User.Queries.GetUsers;
using PArticle.Application.Models.Auth;
using PArticle.Application.Models.User;

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

			CreateMap<CreateUserCommandRequest, Domain.Entities.User>();
			CreateMap<UpdateUserCommandRequest, Domain.Entities.User>();
			CreateMap<Domain.Entities.User, UserResponseModel>();
			CreateMap<UserRequestModel, CreateUserCommandRequest>();
			CreateMap<UserRequestModel, UpdateUserCommandRequest>();

			CreateMap<Domain.Entities.User, CreateUserCommandResponse>();
			CreateMap<Domain.Entities.User, UpdateUserCommandResponse>();


			CreateMap<Domain.Entities.User, GetUserQueryResponse>();

			CreateMap<Domain.Entities.User, GetUsersQueryResponse>();
			CreateMap<UserFilterModel, GetUsersQueryRequest>();

		}
	}
}
