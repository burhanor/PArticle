using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Infrastructure.ElasticSearch
{
	public class ElasticSearchModel
	{
		public string Uri { get; set; }
		public string IndexName { get; set; }
		public string Password { get; set; }
		public string UserName { get; set; }
	}
}
