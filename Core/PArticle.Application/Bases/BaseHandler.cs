using AutoMapper;
using Domain.Contracts.Interfaces;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Repositories;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Extentions;

namespace PArticle.Application.Bases
{
	public class BaseHandler<T> where T : class, IEntityBase, new()
	{
		public readonly IUow uow;
		public readonly IHttpContextAccessor httpContextAccessor;
		public readonly IMapper mapper;
		public readonly int userId = 0;
		public readonly string ipAddress = string.Empty;
		public readonly IReadRepository<T> readRepository;
		public readonly IWriteRepository<T> writeRepository;
		public readonly string languageCode = "tr";
		public readonly IRabbitMqService RabbitMqService;
		public BaseHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper,IRabbitMqService rabbitMqService) : base()
		{
			this.uow = uow;
			this.httpContextAccessor = httpContextAccessor;
			this.mapper = mapper;
			userId = httpContextAccessor.GetUserId();
			ipAddress = httpContextAccessor.GetIpAddress();
			readRepository = uow.GetReadRepository<T>();
			writeRepository = uow.GetWriteRepository<T>();
			RabbitMqService = rabbitMqService;
		}


		public BaseHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base()
		{
			this.uow = uow;
			this.httpContextAccessor = httpContextAccessor;
			this.mapper = mapper;
			userId = httpContextAccessor.GetUserId();
			ipAddress = httpContextAccessor.GetIpAddress();
			readRepository = uow.GetReadRepository<T>();
			writeRepository = uow.GetWriteRepository<T>();
		}
	}

	public class BaseHandler
	{
		public readonly IUow uow;
		public readonly IHttpContextAccessor httpContextAccessor;
		private readonly IMapper mapper;
		public readonly int userId = 0;
		public readonly string ipAddress = string.Empty;
		public BaseHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base()
		{
			this.uow = uow;
			this.httpContextAccessor = httpContextAccessor;
			this.mapper = mapper;
			userId = httpContextAccessor.GetUserId();
			ipAddress = httpContextAccessor.GetIpAddress();
		}


	}
}
