using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Interfaces
{
	public interface IGetByIdRequest<TResponse>:IRequest<TResponse>
	{
		public int Id { get; set; }
	}
}
