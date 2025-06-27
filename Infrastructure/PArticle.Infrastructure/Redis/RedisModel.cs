using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Infrastructure.Redis
{
	public class RedisModel
	{
		public string Host { get; set; }
		public int Port { get; set; }
		public string Password { get; set; }
	}
}
