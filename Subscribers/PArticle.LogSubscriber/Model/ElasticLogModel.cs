using PArticle.Subscribers.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.LogSubscriber.Model
{
	public class ElasticLogModel:IElasticEntity
	{
		public string ElasticId { get; set; } = Guid.NewGuid().ToString();
		public string Message { get; set; } = string.Empty;

		public ElasticLogModel(string message)
		{
			Message = message;
		}
	}
}
