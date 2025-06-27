using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Subscribers.Core.Models
{
	public  class AppConstantModel
	{
		public  string QueueName {get;set; }
		public  string HashName { get; set; }
		public  string SlugPrefix { get; set; }
		public  string AppName { get; set; }
		public  string LogExchangeName { get; set; }
		public  string LogRoutingKey { get; set; }
		public string LogQueueName { get; set; }
	}
}
