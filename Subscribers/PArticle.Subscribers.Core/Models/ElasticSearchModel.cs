using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Subscribers.Core.Models
{
	public class ElasticSearchModel
	{
		public string IndexName { get; set; }
		public string Uri { get; set; }
		public string Password { get; set; }
		public string UserName { get; set; }
	}
}
