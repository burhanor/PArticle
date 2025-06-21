using PArticle.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Concretes
{
	public class GetByIdRequest<TResponse>:IGetByIdRequest<TResponse> where TResponse : class
	{
		public int Id { get; set; }
		public GetByIdRequest()
		{
			
		}
		public GetByIdRequest(int id)
		{
			Id = id;
		}
	}
}
