using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Infrastructure.RabbitMq
{
	public class RabbitMqModel
	{
		public string HOSTNAME { get; set; }
		public int PORT { get; set; }
		public string RABBITMQ_DEFAULT_USER { get; set; }
		public string RABBITMQ_DEFAULT_PASS { get; set; }
	}
}
